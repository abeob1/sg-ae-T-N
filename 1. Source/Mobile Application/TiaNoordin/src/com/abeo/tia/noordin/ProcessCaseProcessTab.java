package com.abeo.tia.noordin;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.PopupWindow;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemSelectedListener;
import android.view.View;

public class ProcessCaseProcessTab extends BaseActivity implements OnClickListener{
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	boolean PastSectionclicked;
	ListView process_case;
	private String m_chosenDir = "";
    private boolean m_newFolderEnabled = true;
    public int clickedposition =-1;
    
    private File selectedFile;
	public String fileName, rtext ;
	
	ProgressDialog dialog = null;
	String BASE_URL = RestService.getBurl();

	private final String METHOD_PROCESS_CASE_DETAILS = "SPA_ProcessCase_GetDataFromOCRD";
	private final String METHOD_ADDFILE = BASE_URL+"Attachments";
	private static final int BUFFER_SIZE = 4096;
	private static final int REQUEST_PICK_FILE = 1;
	private static final int PICK_FILEUPLOAD =2;

	Button purchaser_btn, vendor_btn, property_btn, loan_principal_btn,
			loan_subsidary_btn, process_btn, confirm_btn,
			past_sec, next_sec, open_sec, add_optional, view_file, process_step, btnClosePopup,details_btn,walkin,download,btnbrows2,btnbrows,upload
			,browsepop,uploadpop,savepop,updatestatuspop,createbillingpop,alternativepop;
	
	View vnext_sec, vopen_sec;

	EditText file_open_date, case_fileno, case_type,BROWSER2text2,BROWSER2text1,Remarks;

	// spinner declaration
	Spinner spinner_case_status, spinner_status,spinner_kiv;
	TextView ID, TEXT;

	String caseValue_id = "", titleValue = "", casetype = "",
			casetype_value = "",TrnspNamepop = "",FILESAVELOCATION="",FILEUPLOADRESULT,resultofdownload,statusval="",statusid="",stxt2="",Scase_status,Skiv;
	
	String Genpath,uploadpath,Details,	ActionBy,Status,	LastUpdate ,	StatusDate ,	TrnspName,	ForwardParty ,ItemCode,CASEFILENUMBER,clickedcase,ItemCoderes,
	DocEntryres,DocNumres,LineNumres,details0;

	// Get Project value fromapi
	private final String GET_SPINNER_VALUES = "SPA_GetValidValues";
	ArrayList<HashMap<String, String>> jsonArraylist = null;
	ArrayList<HashMap<String, String>> jsonArraylist_st=null;
	ArrayList<HashMap<String, String>> jsonArraylist_cs=null;
	ArrayList<HashMap<String, String>> jsonArraylistkvi=null;
	
	
	String id, name;
	SimpleAdapter sAdapPROJ;
	
	JSONArray arrayResponse = null,arrayResponse_status=null,arrayResponse_up=null,arrayResponse_sv=null,arrayResponse_st=null,arrayResponse_cs=null;
	JSONObject jsonResponse = null,jsonResponse_status,casedata=null,jsonResponse_gen=null,jsonResponse_up=null,jsonResponse_sv=null,jsonResponse_st=null,jsonResponse_cs=null;
	
	String[] Detailsarray;
	

	JSONArray arrOfJson = new JSONArray();
	ArrayList<HashMap<String, String>> jsonItemList = null,jsonArraylist_status=null;
	String itemCode = "", itemName = "",Resultfile="";
	
	/********** File Path *************/
	int serverResponseCode = 0;	
	
	
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		
		// Find the SharedPreferences value
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
		System.out.println("LOGIN DATA");
		user_name = prefLoginReturn.getString("sUserName", "");
		System.out.println(user_name);
		Pswd = prefLoginReturn.getString("sPassword", "");
		System.out.println(Pswd);
		catg = prefLoginReturn.getString("sCategory", "");
		System.out.println(catg);
		sUserRole = prefLoginReturn.getString("sUserRole", "");
		System.out.println(sUserRole);
		String CardCode = prefLoginReturn.getString("CardCode", "");
		System.out.println(CardCode);
		String CaseNo = prefLoginReturn.getString("CaseNo", "");		
		Genpath = prefLoginReturn.getString("GenPath", "");
		uploadpath = prefLoginReturn.getString("UploadPath", "");
		System.out.println(CaseNo);
		if(CaseNo=="" || CaseNo.isEmpty())
			CASEFILENUMBER = "0";
		else
			CASEFILENUMBER = CaseNo;
		
		
		
		System.out.println("CaseNumberrr");
		System.out.println(CASEFILENUMBER);
		
	
		
		
		
		
		
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_processcase_process);

		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources()
				.obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);

		// Find the SharedPreferences Firstname
					SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);		
					String FirName = FirstName.getString("FIRSETNAME", "");
					TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
					welcome.setText("Welcome "+FirName);
					
					
					

	

		process_case = (ListView) findViewById(R.id.listview_process);

		// spinners initialization
		spinner_case_status = (Spinner) findViewById(R.id.case_status);
		spinner_kiv = (Spinner) findViewById(R.id.spinner_ProcessCase1KIV);

		// edittext
		case_fileno = (EditText) findViewById(R.id.editText_ProcessCase1CaseFileNo);
		case_type = (EditText) findViewById(R.id.case_type);
		file_open_date = (EditText) findViewById(R.id.editText_ProcessCase1FileOpenDate);
		
		//Button init
		past_sec = (Button) findViewById(R.id.button_pastsections);
		next_sec = (Button) findViewById(R.id.button_nextsection);
		open_sec = (Button) findViewById(R.id.button_opensection);
		add_optional = (Button) findViewById(R.id.button_addoptional);
		view_file = (Button) findViewById(R.id.button_viewfile);
		process_step = (Button) findViewById(R.id.button_processstep);
		
		vnext_sec = findViewById(R.id.button_nextsection);
		vopen_sec = findViewById(R.id.button_opensection);
		
		
		//buttons initialization
		details_btn = (Button) findViewById(R.id.button_ProcessCase1Details);
        purchaser_btn = (Button) findViewById(R.id.button_ProcessCase1Purchaser);
        vendor_btn = (Button) findViewById(R.id.button_ProcessCase1Vendor);
        property_btn = (Button) findViewById(R.id.button_ProcesCase1Property);
        loan_principal_btn = (Button) findViewById(R.id.button_ProcessCase1LoanPrincipal);
        loan_subsidary_btn = (Button) findViewById(R.id.button_ProcesCase1LoanSubsidiary);
        process_btn = (Button) findViewById(R.id.button_ProcessCase1Process);
        walkin = (Button) findViewById(R.id.button_ProcessCase1Walkin);
		       
		
		past_sec.setOnClickListener(this);
		next_sec.setOnClickListener(this);
		open_sec.setOnClickListener(this);
		add_optional.setOnClickListener(this);
		view_file.setOnClickListener(this);
		process_step.setOnClickListener(this);
		walkin.setOnClickListener(this);
		
		details_btn.setOnClickListener(this);
        purchaser_btn.setOnClickListener(this);
        vendor_btn.setOnClickListener(this);
        property_btn.setOnClickListener(this);
        loan_principal_btn.setOnClickListener(this);
        loan_subsidary_btn.setOnClickListener(this);
        process_btn.setOnClickListener(this);
		
		try {
			setRequestData();			
			dropdownstatus();			
			opensection();
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		process_step.setEnabled(false);
		process_step.setClickable(false);
		
		//next_sec.setEnabled(false);
		//next_sec.setClickable(false);
		
		
		
		view_file.setEnabled(true);
		view_file.setClickable(true);
		
		disableHeaderfields();
		
		
		process_case.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
		process_case.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			View ls=null;
			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				
				if(get()!=view && get()!=null)
				{
					get().setActivated(false);
				}
				
				
				//Toast.makeText(ProcessCaseProcessTab.this, "You Clicked at " + jsonArraylist.get(position).get("ActionBy"),
					//	Toast.LENGTH_SHORT).show();
				clickedposition = position;
				
				clickedcase = jsonArraylist.get(position).toString();
				System.out.println(clickedcase);
				view.setActivated(true);
				System.out.println("clicked data");
				TrnspNamepop=jsonArraylist.get(position).get("TrnspName");
				ItemCoderes=jsonArraylist.get(position).get("ItemCode");
				DocEntryres=jsonArraylist.get(position).get("DocEntry");
				DocNumres=jsonArraylist.get(position).get("DocNum");
				LineNumres=jsonArraylist.get(position).get("LineNum");
				details0=jsonArraylist.get(position).get("Details0");
				stxt2=jsonArraylist.get(position).get("TrnspName");				
				set(view);
				String u = jsonArraylist.get(position).get("ActionBy");
				if((sUserRole.equals(u) && PastSectionclicked!=true))
				{
					process_step.setEnabled(true);
					process_step.setClickable(true);					
				}
				else if((sUserRole.equals("LA") && PastSectionclicked!=true && u.equals("MG")))
				{
					process_step.setEnabled(true);
					process_step.setClickable(true);					
				}
				else if((sUserRole.equals("MG") && PastSectionclicked!=true && u.equals("LA")))
				{
					process_step.setEnabled(true);
					process_step.setClickable(true);					
				}
				else 
				{
					process_step.setEnabled(false);
					process_step.setClickable(false);	
				}
				

			}
			
			private void set(View view) {						
				ls=view;						
			}
			private View get() {									
				return ls;
			}

			
		});
		
		
		
		// Spinner click listener
				spinner_case_status
						.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

							@Override
							public void onItemSelected(AdapterView<?> parent,
									View view, int position, long id) {
								ID = (TextView) view.findViewById(R.id.Id);
								caseValue_id = ID.getText().toString();
								TEXT = (TextView) view.findViewById(R.id.Name);
								titleValue = TEXT.getText().toString();
							}

							@Override
							public void onNothingSelected(AdapterView<?> parent) {
								// TODO Auto-generated method stub

							}
						});
				
				
				

				

	}
	
	
			
		
	private void disableHeaderfields() {
		// TODO Auto-generated method stub
				
				file_open_date.setEnabled(false);
		        case_type.setEnabled(false);
		        case_fileno.setEnabled(false); 
		        spinner_case_status.setEnabled(false);
		        spinner_kiv.setEnabled(false);
		
	}

	public void viewfile()
	{
		System.out.println(clickedposition);
		if(clickedposition==-1)
		{
			slog("No Files Avilable to Display.");
		}
		else
		{
		
		String pdflink1 = jsonArraylist.get(clickedposition).get("Resultfile");
		System.out.println(jsonArraylist.get(clickedposition));
		
		if(!pdflink1.isEmpty())
		   {
			 String googleDocsUrl;
			 String filenameArray[] = pdflink1.split("\\.");
		        String extension = filenameArray[filenameArray.length-1];
				if(extension.equals("pdf"))
					googleDocsUrl = "http://docs.google.com/viewer?url=http://54.251.51.69:3878/"+ pdflink1;
				else			 
					googleDocsUrl = "http://54.251.51.69:3878"+pdflink1;
		   
		   Intent intent = new Intent(Intent.ACTION_VIEW);
		   intent.setDataAndType(Uri.parse(googleDocsUrl ), "text/html");
		   startActivity(intent);
		   }
		 else
		 {
			 slog("No Files Avilable to Display.");
		 }
		}
		
		
	}
	
	public void dropdownKIV() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "OCRD");
		jsonObject.put("FieldName", "KIVSTATUS");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(GET_SPINNER_VALUES, params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");

					}

					@Override
					protected String parseResponse(String s, boolean b)
							throws Throwable {
						return null;
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out.println("Title Dropdown Success Details ");
						System.out.println(arg2);

						try {

							arrayResponse = new JSONArray(arg2);
							// Create new list
							jsonArraylistkvi = new ArrayList<HashMap<String, String>>();

							for (int i = 0; i < arrayResponse.length(); i++) {

								jsonResponse = arrayResponse.getJSONObject(i);

								id = jsonResponse.getString("Id").toString();
								name = jsonResponse.getString("Name")
										.toString();

								// SEND JSON DATA INTO SPINNER TITLE LIST
								HashMap<String, String> proList = new HashMap<String, String>();

								// Send JSON Data to list activity
								System.out.println("SEND JSON  LIST");
								proList.put("Id_T", id);
								System.out.println(name);
								proList.put("Name_T", name);
								System.out.println(name);
								System.out
										.println(" END SEND JSON PROPERTY LIST");

								jsonArraylistkvi.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylistkvi);
							}
							// Spinner set Array Data in Drop down

							sAdapPROJ = new SimpleAdapter(
									ProcessCaseProcessTab.this, jsonArraylistkvi,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_kiv.setAdapter(sAdapPROJ);

							System.out.println("Skiv");
							System.out.println(Skiv);
							for (int j = 0; j < jsonArraylistkvi.size(); j++)
							  { if (jsonArraylistkvi.get(j).get("Id_T").equals(Skiv)) {
								  spinner_kiv.setSelection(j); break; } }

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}
			
    public void dropdownstatus() throws JSONException {
        RequestParams params = null;
        params = new RequestParams();

        JSONObject jsonObject = new JSONObject();
        jsonObject.put("TableName", "OCRD");
        jsonObject.put("FieldName", "CASESTATUS");
        params.put("sJsonInput", jsonObject.toString());

        RestService.post(GET_SPINNER_VALUES, params, new BaseJsonHttpResponseHandler<String>() {

            @Override
            public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
                // TODO Auto-generated method stub
                System.out.println(arg3);
                System.out.println("Failed");

            }

            @Override
            protected String parseResponse(String s, boolean b) throws Throwable {
                return null;
            }

            @Override
            public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
                // TODO Auto-generated method stub
                System.out.println("Title Dropdown Success Details ");
                System.out.println(arg2);

                try {

                    arrayResponse_cs = new JSONArray(arg2);
                    // Create new list
                    jsonArraylist_cs = new ArrayList<HashMap<String, String>>();

                    for (int i = 0; i < arrayResponse_cs.length(); i++) {

                        jsonResponse_cs = arrayResponse_cs.getJSONObject(i);

                        id = jsonResponse_cs.getString("Id").toString();
                        name = jsonResponse_cs.getString("Name").toString();

                        // SEND JSON DATA INTO SPINNER TITLE LIST
                        HashMap<String, String> proList = new HashMap<String, String>();

                        // Send JSON Data to list activity
                        System.out.println("SEND JSON  LIST");
                        proList.put("Id_T", id);
                        System.out.println(name);
                        proList.put("Name_T", name);
                        System.out.println(name);
                        System.out.println(" END SEND JSON PROPERTY LIST");

                        jsonArraylist_cs.add(proList);
                        System.out.println("JSON PROPERTY LIST");
                        System.out.println(jsonArraylist);
                    }
                    // Spinner set Array Data in Drop down

                    sAdapPROJ = new SimpleAdapter(ProcessCaseProcessTab.this, jsonArraylist_cs, R.layout.spinner_item,
                            new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

                    spinner_case_status.setAdapter(sAdapPROJ);
                    
                 // Find the SharedPreferences Firstname
					SharedPreferences fn = getSharedPreferences("KVIData", Context.MODE_PRIVATE);		
					Scase_status = fn.getString("CaseStatus", "");
					Skiv = fn.getString("KIV", "");
					System.out.println("Skiv data");
					System.out.println(Skiv);

					for (int j = 0; j < jsonArraylist_cs.size(); j++)
					  { if (jsonArraylist_cs.get(j).get("Id_T").equals(
							  Scase_status)) {
						  spinner_case_status.setSelection(j); break; } }			
					
					

                } catch (JSONException e) {

                    e.printStackTrace();
                }
            }
        });
    }
    
	public void opensection() throws JSONException {
		
		PastSectionclicked = false;
		clickedposition = -1;
		
		view_file.setEnabled(true);
		view_file.setClickable(true);
		
				RequestParams params = null;
				params = new RequestParams();

				JSONObject jsonObject = new JSONObject();
				jsonObject.put("sCaseNo", CASEFILENUMBER);
				params.put("sJsonInput", jsonObject.toString());
				
				System.out.println(params);

				RestService.post("SPA_ProcessCase_GetOpenSection", params,
						new BaseJsonHttpResponseHandler<String>() {

							@Override
							public void onFailure(int arg0, Header[] arg1,
									Throwable arg2, String arg3, String arg4) {
								// TODO Auto-generated method stub
								System.out.println(arg3);
								System.out.println("Failed");
								
								

							}

							@Override
							protected String parseResponse(String s, boolean b)
									throws Throwable {
								return null;
							}

							@Override
							public void onSuccess(int arg0, Header[] arg1, String arg2,
									String arg3) {
								
								// TODO Auto-generated method stub
								System.out.println("List App Dropdown Success Details ");
								System.out.println(arg2);
								

								try {
									
									

									arrayResponse = new JSONArray(arg2);
									// Create new list
									jsonArraylist = new ArrayList<HashMap<String, String>>();
									 
									
									for (int i = 0; i < arrayResponse.length(); i++) {

										jsonResponse = arrayResponse.getJSONObject(i);

										Detailsarray =jsonResponse.getString("Details").toString().split("--n--");										
										ActionBy = jsonResponse.getString("ActionBy").toString();
										Status = jsonResponse.getString("Status").toString();
										LastUpdate = jsonResponse.getString("LastUpdate").toString();
										StatusDate = jsonResponse.getString("StatusDate").toString();
										TrnspName = jsonResponse.getString("TrnspName").toString();
										ForwardParty = jsonResponse.getString("ForwardParty").toString();
										ItemCode = jsonResponse.getString("ItemCode").toString();
										
										

										// SEND JSON DATA INTO SPINNER TITLE LIST
										HashMap<String, String> proList = new HashMap<String, String>();

										// Send JSON Data to list activity
										System.out.println("SEND JSON  LIST");
										
										if(Detailsarray.length>=1)
											proList.put("Details0", Detailsarray[0]);
										else
											proList.put("Details0", "");
										
										if(Detailsarray.length>=2)
											proList.put("Details1", Detailsarray[1]);
										else
											proList.put("Details1", "");
										
										if(Detailsarray.length>=3)
											proList.put("Details2", Detailsarray[2]);
										else
											proList.put("Details2", "");
										
										proList.put("ActionBy", ActionBy);
										proList.put("Status", Status);
										proList.put("LastUpdate", LastUpdate);
										proList.put("StatusDate", StatusDate);
										proList.put("TrnspName", TrnspName);
										proList.put("ForwardParty", ForwardParty);
										proList.put("ItemCode", ItemCode);
										proList.put("DocEntry", jsonResponse.getString("DocEntry").toString());
										proList.put("DocNum", jsonResponse.getString("DocNum").toString());
										proList.put("LineNum", jsonResponse.getString("LineNum").toString());
										proList.put("Resultfile", jsonResponse.getString("ResultFile").toString());
										
										
										System.out
												.println(" END SEND JSON PROPERTY LIST");

										jsonArraylist.add(proList);
										System.out.println("JSON PROPERTY LIST");
										System.out.println(jsonArraylist);
									}
									
									// Simple Adapter for List
									SimpleAdapter simpleAdapter = new SimpleAdapter(ProcessCaseProcessTab.this, jsonArraylist,
											R.layout.listview_column_process_itemlist, new String[] { "Details0","Details1","Details2", "ActionBy","Status","LastUpdate","StatusDate","TrnspName","ForwardParty" },
											new int[] { R.id.Details0,R.id.Details1,R.id.Details2, R.id.ActionBy, R.id.Status, R.id.LastUpdate, R.id.StatusDate, R.id.TrnspName, R.id.ForwardParty });

									process_case.setAdapter(simpleAdapter);
									// Spinner set Array Data in Drop down

									

								} catch (JSONException e) {

									e.printStackTrace();
								}
								
								
							}
						});
			}
			
	public void PastSection() throws JSONException {
		
		PastSectionclicked = true;
		clickedposition = -1;
		
		TextView lab = (TextView)findViewById(R.id.textview_processname);		
		lab.setText("Past process and Remarks");
		
		
				RequestParams params = null;
				params = new RequestParams();

				JSONObject jsonObject = new JSONObject();
				jsonObject.put("sCaseNo", CASEFILENUMBER);
				params.put("sJsonInput", jsonObject.toString());

				RestService.post("SPA_ProcessCase_GetPastSection", params,
						new BaseJsonHttpResponseHandler<String>() {

							@Override
							public void onFailure(int arg0, Header[] arg1,
									Throwable arg2, String arg3, String arg4) {
								// TODO Auto-generated method stub
								System.out.println(arg3);
								System.out.println("Failed");
								dialog.dismiss();

							}

							@Override
							protected String parseResponse(String s, boolean b)
									throws Throwable {
								return null;
							}

							@Override
							public void onSuccess(int arg0, Header[] arg1, String arg2,
									String arg3) {
								// TODO Auto-generated method stub
								System.out.println("List App Dropdown Success Details ");
								System.out.println(arg2);

								try {
									
									
									

									arrayResponse = new JSONArray(arg2);
									// Create new list
									jsonArraylist = new ArrayList<HashMap<String, String>>();
									
									
									for (int i = 0; i < arrayResponse.length(); i++) {

										jsonResponse = arrayResponse.getJSONObject(i);

										Detailsarray =jsonResponse.getString("Details").toString().split("--n--");
										ActionBy = jsonResponse.getString("ActionBy").toString();
										Status = jsonResponse.getString("Status").toString();
										LastUpdate = jsonResponse.getString("LastUpdate").toString();
										StatusDate = jsonResponse.getString("StatusDate").toString();
										TrnspName = jsonResponse.getString("TrnspName").toString();
										ForwardParty = jsonResponse.getString("ForwardParty").toString();
										ItemCode = jsonResponse.getString("ItemCode").toString();
										Resultfile = jsonResponse.getString("ResultFile").toString();
										

										// SEND JSON DATA INTO SPINNER TITLE LIST
										HashMap<String, String> proList = new HashMap<String, String>();

										// Send JSON Data to list activity
										System.out.println("SEND JSON  LIST");
										
										if(Detailsarray.length>=1)
											proList.put("Details0", Detailsarray[0]);
										else
											proList.put("Details0", "");
										
										if(Detailsarray.length>=2)
											proList.put("Details1", Detailsarray[1]);
										else
											proList.put("Details1", "");
										
										if(Detailsarray.length>=3)
											proList.put("Details2", Detailsarray[2]);
										else
											proList.put("Details2", "");
										
										proList.put("ActionBy", ActionBy);
										proList.put("Status", Status);
										proList.put("LastUpdate", LastUpdate);
										proList.put("StatusDate", StatusDate);
										proList.put("TrnspName", TrnspName);
										proList.put("ForwardParty", ForwardParty);
										proList.put("ItemCode", ItemCode);
										proList.put("DocEntry", jsonResponse.getString("DocEntry").toString());
										proList.put("DocNum", jsonResponse.getString("DocNum").toString());
										proList.put("LineNum", jsonResponse.getString("LineNum").toString());
										proList.put("Resultfile", Resultfile);
										
										System.out
												.println(" END SEND JSON PROPERTY LIST");

										jsonArraylist.add(proList);
										System.out.println("JSON PROPERTY LIST");
										System.out.println(jsonArraylist);
									}
									
									// Simple Adapter for List
									SimpleAdapter simpleAdapter = new SimpleAdapter(ProcessCaseProcessTab.this, jsonArraylist,
											R.layout.listview_column_process_itemlist, new String[] { "Details0","Details1","Details2", "ActionBy","Status","LastUpdate","StatusDate","TrnspName","ForwardParty" },
											new int[] { R.id.Details0,R.id.Details1,R.id.Details2, R.id.ActionBy, R.id.Status, R.id.LastUpdate, R.id.StatusDate, R.id.TrnspName, R.id.ForwardParty });

									process_case.setAdapter(simpleAdapter);
									// Spinner set Array Data in Drop down

									

								} catch (JSONException e) {

									e.printStackTrace();
								}
								dialog.dismiss();
							}
						});
			}
			
	public void GetNextSection() throws JSONException {
		
		PastSectionclicked = false;
		clickedposition = -1;
		
		TextView lab = (TextView)findViewById(R.id.textview_processname);		
		lab.setText("Next process and Remarks");
		
		process_step.setEnabled(false);
		process_step.setClickable(false);
		view_file.setEnabled(true);
		view_file.setClickable(true);
		
		
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("sCaseNo", CASEFILENUMBER);
		params.put("sJsonInput", jsonObject.toString());

		RestService.post("SPA_ProcessCase_GetNextSection", params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");
						dialog.dismiss();

					}

					@Override
					protected String parseResponse(String s, boolean b)
							throws Throwable {
						return null;
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out.println("List App Dropdown Success Details ");
						System.out.println(arg2);

						try {
							
							
							

							arrayResponse = new JSONArray(arg2);
							// Create new list
							jsonArraylist = new ArrayList<HashMap<String, String>>();
							
							
							for (int i = 0; i < arrayResponse.length(); i++) {

								jsonResponse = arrayResponse.getJSONObject(i);

								Detailsarray =jsonResponse.getString("Details").toString().split("--n--");
								ActionBy = jsonResponse.getString("ActionBy").toString();
								Status = jsonResponse.getString("Status").toString();
								LastUpdate = jsonResponse.getString("LastUpdate").toString();
								StatusDate = jsonResponse.getString("StatusDate").toString();
								TrnspName = jsonResponse.getString("TrnspName").toString();
								ForwardParty = jsonResponse.getString("ForwardParty").toString();
								ItemCode = jsonResponse.getString("ItemCode").toString();
								

								// SEND JSON DATA INTO SPINNER TITLE LIST
								HashMap<String, String> proList = new HashMap<String, String>();

								// Send JSON Data to list activity
								System.out.println("SEND JSON  LIST");
								
								if(Detailsarray.length>=1)
									proList.put("Details0", Detailsarray[0]);
								else
									proList.put("Details0", "");
								
								if(Detailsarray.length>=2)
									proList.put("Details1", Detailsarray[1]);
								else
									proList.put("Details1", "");
								
								if(Detailsarray.length>=3)
									proList.put("Details2", Detailsarray[2]);
								else
									proList.put("Details2", "");
								
								proList.put("ActionBy", ActionBy);
								proList.put("Status", Status);
								proList.put("LastUpdate", LastUpdate);
								proList.put("StatusDate", StatusDate);
								proList.put("TrnspName", TrnspName);
								proList.put("ForwardParty", ForwardParty);
								proList.put("ItemCode", ItemCode);
								proList.put("DocEntry", jsonResponse.getString("DocEntry").toString());
								proList.put("DocNum", jsonResponse.getString("DocNum").toString());
								proList.put("LineNum", jsonResponse.getString("LineNum").toString());
								proList.put("Resultfile", jsonResponse.getString("ResultFile").toString());
								
								System.out
										.println(" END SEND JSON PROPERTY LIST");

								jsonArraylist.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylist);
							}
							
							// Simple Adapter for List
							SimpleAdapter simpleAdapter = new SimpleAdapter(ProcessCaseProcessTab.this, jsonArraylist,
									R.layout.listview_column_process_itemlist, new String[] { "Details0","Details1","Details2", "ActionBy","Status","LastUpdate","StatusDate","TrnspName","ForwardParty" },
									new int[] { R.id.Details0,R.id.Details1,R.id.Details2, R.id.ActionBy, R.id.Status, R.id.LastUpdate, R.id.StatusDate, R.id.TrnspName, R.id.ForwardParty });

							process_case.setAdapter(simpleAdapter);
							// Spinner set Array Data in Drop down

							

						} catch (JSONException e) {

							e.printStackTrace();
						}
						dialog.dismiss();
					}
					
				});
	}
	public void spinner_statusdata() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();
		
		RestService.post("SPA_ProcessCase_GetStatus", params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");

					}

					@Override
					protected String parseResponse(String s, boolean b)
							throws Throwable {
						return null;
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out.println("Status Dropdown Success Details ");
						System.out.println(arg2);

						try {

							arrayResponse_st = new JSONArray(arg2);
							// Create new list
							jsonArraylist_st = new ArrayList<HashMap<String, String>>();

							for (int i = 0; i < arrayResponse_st.length(); i++) {

								jsonResponse_st = arrayResponse_st.getJSONObject(i);

								id = jsonResponse_st.getString("Id").toString();
								name = jsonResponse_st.getString("Name")
										.toString();

								// SEND JSON DATA INTO SPINNER TITLE LIST
								HashMap<String, String> proList = new HashMap<String, String>();

								// Send JSON Data to list activity
								System.out.println("SEND JSON  LIST");
								proList.put("Id_T", id);
								System.out.println(name);
								proList.put("Name_T", name);
								System.out.println(name);
								System.out
										.println(" END SEND JSON PROPERTY LIST");

								jsonArraylist_st.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylist);
							}
							// Spinner set Array Data in Drop down

							sAdapPROJ = new SimpleAdapter(
									ProcessCaseProcessTab.this, jsonArraylist_st,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_status.setAdapter(sAdapPROJ);

							/*
							 * for (int j = 0; j < jsonlistProject.size(); j++)
							 * { if (jsonlistProject.get(j).get("Id_T").equals(
							 * projectDetailResponse)) {
							 * TitleType_DROPDOWN.setSelection(j); break; } }
							 */

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}
	private void setRequestData() throws JSONException {
		dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
		RequestParams params = null;
		params = new RequestParams();
		// Find the SharedPreferences value
				SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
				System.out.println("LOGIN DATA");
				user_name = prefLoginReturn.getString("sUserName", "");
				System.out.println(user_name);
				Pswd = prefLoginReturn.getString("sPassword", "");
				System.out.println(Pswd);
				catg = prefLoginReturn.getString("sCategory", "");
				System.out.println(catg);
				sUserRole = prefLoginReturn.getString("sUserRole", "");
				System.out.println(sUserRole);
				String sCaseNo = prefLoginReturn.getString("CaseNo", "");
				System.out.println(sCaseNo);

		        final JSONObject jsonObject = new JSONObject();
		        jsonObject.put("CaseNo", sCaseNo);
		        jsonObject.put("UserName", user_name);
		        jsonObject.put("UserRole", sUserRole);
		        params.put("sJsonInput", jsonObject.toString());

		RestService.post(METHOD_PROCESS_CASE_DETAILS, params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");
						dialog.dismiss();

					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out
								.println("property Dropdown Success Details ");
						System.out.println(arg2);
						dialog.dismiss();
						try {
						setallvalues(arg2);
						dropdownKIV();
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}

					}

					@Override
					protected String parseResponse(String arg0, boolean arg1)
							throws Throwable {

						// Get Json response
						JSONArray arrayResponse = new JSONArray(arg0);
						jsonResponse = arrayResponse.getJSONObject(0);

						System.out
								.println("Property Dropdown Details parse Response");
						System.out.println(arg0);
						return null;
					}
				});

	}
	private void setallvalues(String arg2) {
		//pwindo.dismiss();

		JSONArray jObj = null;
		try {
			jObj = new JSONArray(arg2.toString());
			
			//JSONObject jsonobjectkv = jObj.getJSONObject(0);
			//Skiv = jsonobjectkv.getString("KIV");
			for (int i = 0; i < jObj.length(); i++) {
				JSONObject jsonobject = jObj.getJSONObject(i);
				file_open_date.setText(jsonobject.getString("FileOpenDate"));
				case_fileno.setText(jsonobject.getString("CaseFileNo"));
				case_type.setText(jsonobject.getString("CaseType"));
				Skiv = jsonobject.getString("KIV");
				

				
			}
			System.out.println(jObj);
		} catch (JSONException e) {
			e.printStackTrace();
		}
	}
	private void updatealternative(JSONObject array)
	{
		dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
		try {
		RequestParams params = null;
		params = new RequestParams();
		
		JSONObject jsonObject = new JSONObject();		
				
		jsonObject.put("sOldItemCode", ItemCoderes);
		jsonObject.put("sNewItemCode", array.get("ItemCode").toString());
		jsonObject.put("sItemName", array.get("ItemName").toString());
		jsonObject.put("sDocEntry", DocEntryres);
		jsonObject.put("sLineNum", LineNumres);
					
		params.put("sJsonInput", jsonObject.toString());
		
		final String returnarg;

		RestService.post("SPA_ProcessCase_UpdateAlternative", params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
				System.out.println("Failed");
				dialog.dismiss();

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Update Success Details ");
				System.out.println(arg2);
				dialog.dismiss();
				
				try {
					arrayResponse = new JSONArray(arg2);
				
				

					jsonResponse = arrayResponse.getJSONObject(0);

					String rerult = jsonResponse.getString("Result").toString();
					if(rerult.equals("SUCCESS"))
					{
						slog(jsonResponse.getString("DisplayMessage").toString());
						finish();
						startActivity(getIntent());
					}
					
					
				
				
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				};

			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {

				// Get Json response
				arrayResponse = new JSONArray(arg0);
				jsonResponse = arrayResponse.getJSONObject(0);

				System.out.println("Corprate Dropdown Details parse Response");
				System.out.println(arg0);
				return null;
			}		
		});
		} catch (JSONException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}	
	}
	//additem to exsisting list
	private void addvalues(JSONObject array) throws JSONException{
		pwindo.dismiss();
		//String[] Detailsarray = array.getString("Details").toString().split("--n--");
		ActionBy = array.getString("ActionBy").toString();
		//Status = array.getString("Status").toString();
		//LastUpdate = array.getString("LastUpdate").toString();
		String IntNo = array.getString("IntNo").toString();
		String Price = array.getString("Price").toString();
		String Qty = array.getString("Qty").toString();
		ItemCode = array.getString("ItemCode").toString();

		
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("CaseNo", CASEFILENUMBER);
		jsonObject.put("ItemCode", ItemCode);
		jsonObject.put("Qty", Qty);
		jsonObject.put("Price", Price);
		jsonObject.put("ActionBy", ActionBy);
		jsonObject.put("IntNo", IntNo);
		
		params.put("sJsonInput", jsonObject.toString());
		dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
		RestService.post("SPA_ProcessCase_SaveOptionalItems", params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");
						dialog.dismiss();

					}

					@Override
					protected String parseResponse(String s, boolean b)
							throws Throwable {
						return null;
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out.println("Optional data Success Details ");
						System.out.println(arg2);
						String messageDisplay = null;
						dialog.dismiss();

						try {
							
							
							

							arrayResponse = new JSONArray(arg2);
							// Create new list
							jsonArraylist = new ArrayList<HashMap<String, String>>();
							
							jsonResponse = arrayResponse.getJSONObject(0);
							
							
							try {
								//StatusResult = jsonResponse.getString("Result").toString();
								messageDisplay = jsonResponse.getString("DisplayMessage").toString();
							} catch (JSONException e) {
								// TODO Auto-generated catch block
								e.printStackTrace();
							}

						} catch (JSONException e) {

							e.printStackTrace();
						}
						
						
						
						Toast.makeText(ProcessCaseProcessTab.this, messageDisplay, Toast.LENGTH_LONG).show();
						//dialog.dismiss();
						finish();
						startActivity(getIntent());
					}
				});
	
	
	}
	
	private PopupWindow pwindo;
	private void initpopforoptions() {
		// TODO Auto-generated method stub
		final ListView listview_corp;
		try {
		// We need to get the instance of the LayoutInflater
		LayoutInflater inflater = (LayoutInflater) ProcessCaseProcessTab.this	.getSystemService(getApplicationContext().LAYOUT_INFLATER_SERVICE);
		View layout = inflater.inflate(R.layout.popup_optional,(ViewGroup) findViewById(R.id.popup_element),false);
		pwindo = new PopupWindow(layout, 1600, 1070, true);
		pwindo.setOutsideTouchable(true);
		pwindo.showAtLocation(layout, Gravity.CENTER, 0, 0);
		System.out.println("PopUpopstnal Inovaked");

		btnClosePopup = (Button) layout.findViewById(R.id.btn_close_popup);
		btnClosePopup.setOnClickListener(cancel_button_click_listener);

		listview_corp = (ListView) layout.findViewById(R.id.listview_corp);
		
		listview_corp.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			View ls=null;
			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				
				
				if(get()!=view && get()!=null)
				{
					get().setActivated(false);
				}
				try {
					addvalues((JSONObject) arrayResponse.get(position));
					
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				System.out.println(position);

				//int mSelectedItem = position;		
				 	
				 view.setActivated(true);
				 
				/* Get list of Item data
				TextView c = (TextView) view.findViewById(R.id.listAddCaseHeader_ItemCodeText);
				itemCode = c.getText().toString();
				System.out.println(itemCode);
				String data = (String) parent.getItemAtPosition(position).toString();
				System.out.println(data);*/
				set(view);

			}

			private void set(View view) {						
				ls=view;						
			}
			private View get() {									
				return ls;
			}
		});
		
		/*//getcrop */



				RequestParams params = null;
				params = new RequestParams();
				
				JSONObject jsonObject = new JSONObject();
				jsonObject.put("sCaseNo", "1500000001");				
				params.put("sJsonInput", jsonObject.toString());
				
				final String returnarg;

				RestService.post("SPA_ProcessCase_GetOptionalItems", params, new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");

					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
						// TODO Auto-generated method stub
						System.out.println("GetOpitanal Success Details ");
						System.out.println(arg2);
						
						try {
							arrayResponse = new JSONArray(arg2);
						
						// Create new list
						jsonItemList = new ArrayList<HashMap<String, String>>();
						 
						System.out.println("PopUp Inovaked");
						for (int i = 0; i < arrayResponse.length(); i++) {
							
							//JSONObject leagueData = arrayResponse.getJSONObject(i);

							jsonResponse = arrayResponse.getJSONObject(i);

							itemCode = jsonResponse.getString("ItemCode").toString();
							itemName = jsonResponse.getString("Details").toString();

							// SEND JSON DATA INTO SPINNER TITLE LIST
							HashMap<String, String> addCaseItemList = new HashMap<String, String>();

							// Send JSON Data to list activity
							System.out.println("SEND ADD CASE ITEM LIST");
							addCaseItemList.put("ItemCode_T", itemCode);
							System.out.println(itemCode);
							addCaseItemList.put("ItemName_T", itemName);
							System.out.println(itemName);
							System.out.println(" END ADD CASE ITEM LIST");

							jsonItemList.add(addCaseItemList);
							System.out.println("JSON ADD CASE ITEM LIST");
							System.out.println(jsonItemList);	
							
						}
						
						// Simple Adapter for List
						SimpleAdapter simpleAdapter = new SimpleAdapter(ProcessCaseProcessTab.this, jsonItemList,
								R.layout.listview_column_addcase_itemlist, new String[] { "ItemCode_T", "ItemName_T" },
								new int[] { R.id.listAddCaseHeader_ItemCodeText, R.id.listAddCaseHeader_ItemNameText });

						listview_corp.setAdapter(simpleAdapter);
						
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						};

					}

					@Override
					protected String parseResponse(String arg0, boolean arg1) throws Throwable {

						// Get Json response
						arrayResponse = new JSONArray(arg0);
						jsonResponse = arrayResponse.getJSONObject(0);

						System.out.println("Corprate Dropdown Details parse Response");
						System.out.println(arg0);
						return null;
					}		
				});

				} catch (Exception e) {
				e.printStackTrace();
				}
	}
	
	private PopupWindow pwindo1;
	private void alternativepopup() {
		// TODO Auto-generated method stub
		final ListView listview_corp;
		try {
		// We need to get the instance of the LayoutInflater
		LayoutInflater inflater = (LayoutInflater) ProcessCaseProcessTab.this	.getSystemService(getApplicationContext().LAYOUT_INFLATER_SERVICE);
		View layout = inflater.inflate(R.layout.popup_alternative,(ViewGroup) findViewById(R.id.popup_element),false);
		pwindo1 = new PopupWindow(layout, 1600, 1070, true);
		pwindo1.setOutsideTouchable(true);
		pwindo1.showAtLocation(layout, Gravity.CENTER, 0, 0);
		System.out.println("Popalternative Inovaked");

		Button btnClosePopup1 = (Button) layout.findViewById(R.id.btn_close_popup1);
		btnClosePopup1.setOnClickListener(cancel_button_click_listener1);
		

		listview_corp = (ListView) layout.findViewById(R.id.listview_corp);
		
		listview_corp.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			View ls=null;
			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				
				
				if(get()!=view && get()!=null)
				{
					get().setActivated(false);
				}
				try {
					updatealternative((JSONObject) arrayResponse.get(position));
					
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				System.out.println(position);

				//int mSelectedItem = position;		
				 	
				 view.setActivated(true);
				 
				/* Get list of Item data
				TextView c = (TextView) view.findViewById(R.id.listAddCaseHeader_ItemCodeText);
				itemCode = c.getText().toString();
				System.out.println(itemCode);
				String data = (String) parent.getItemAtPosition(position).toString();
				System.out.println(data);*/
				set(view);

			}

			private void set(View view) {						
				ls=view;						
			}
			private View get() {									
				return ls;
			}
		});
		
		/*//getcrop */



				RequestParams params = null;
				params = new RequestParams();
				
				JSONObject jsonObject = new JSONObject();
				jsonObject.put("ItemCode", ItemCoderes);				
				params.put("sJsonInput", jsonObject.toString());
				System.out.println(params);
				final String returnarg;

				RestService.post("SPA_ProcessCase_GetAlternative", params, new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");

					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
						// TODO Auto-generated method stub
						System.out.println("Getalternative Success Details ");
						System.out.println(arg2);
						
						try {
							arrayResponse = new JSONArray(arg2);
						
						// Create new list
						jsonItemList = new ArrayList<HashMap<String, String>>();
						 
						System.out.println("PopUpalternative Inovaked");
						for (int i = 0; i < arrayResponse.length(); i++) {
							
							//JSONObject leagueData = arrayResponse.getJSONObject(i);

							jsonResponse = arrayResponse.getJSONObject(i);

							itemCode = jsonResponse.getString("ItemCode").toString();
							itemName = jsonResponse.getString("ItemName").toString();

							// SEND JSON DATA INTO SPINNER TITLE LIST
							HashMap<String, String> addCaseItemList = new HashMap<String, String>();

							// Send JSON Data to list activity
							System.out.println("SEND ADD CASE ITEM LIST");
							addCaseItemList.put("ItemCode_T", itemCode);
							System.out.println(itemCode);
							addCaseItemList.put("ItemName_T", itemName);
							System.out.println(itemName);
							System.out.println(" END ADD CASE ITEM LIST");

							jsonItemList.add(addCaseItemList);
							System.out.println("JSON ADD CASE ITEM LIST");
							System.out.println(jsonItemList);	
							
						}
						
						// Simple Adapter for List
						SimpleAdapter simpleAdapter = new SimpleAdapter(ProcessCaseProcessTab.this, jsonItemList,
								R.layout.listview_column_addcase_itemlist, new String[] { "ItemCode_T", "ItemName_T" },
								new int[] { R.id.listAddCaseHeader_ItemCodeText, R.id.listAddCaseHeader_ItemNameText });

						listview_corp.setAdapter(simpleAdapter);
						
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						};

					}

					@Override
					protected String parseResponse(String arg0, boolean arg1) throws Throwable {

						// Get Json response
						arrayResponse = new JSONArray(arg0);
						jsonResponse = arrayResponse.getJSONObject(0);

						System.out.println("Corprate Dropdown Details parse Response");
						System.out.println(arg0);
						return null;
					}		
				});

				} catch (Exception e) {
				e.printStackTrace();
				}
	}
	
	private PopupWindow pwindo2;
	private void initpopforsteps() {
		// TODO Auto-generated method stub
		
	
		// TODO Auto-generated method stub
				//final ListView listview_corp;
				
				// We need to get the instance of the LayoutInflater
				LayoutInflater inflater = (LayoutInflater) ProcessCaseProcessTab.this	.getSystemService(getApplicationContext().LAYOUT_INFLATER_SERVICE);
				View layout = inflater.inflate(R.layout.popup_process_step,(ViewGroup) findViewById(R.id.popup_element),false);

				pwindo2 = new PopupWindow(layout, 1600, 1070, true);
				pwindo2.setOutsideTouchable(true);
				pwindo2.showAtLocation(layout, Gravity.CENTER, 0, 0);
				System.out.println("PopUpfffff Inovaked");
				
				
				BROWSER2text2 = (EditText) layout.findViewById(R.id.BROWSER2text);
				BROWSER2text1 = (EditText) layout.findViewById(R.id.uploadtext);
				Remarks = (EditText) layout.findViewById(R.id.remarkss);
				 TextView t1 = (TextView) layout.findViewById(R.id.txt1);
				 TextView t2=(TextView) layout.findViewById(R.id.txt2);
				 t1.setText(details0);
				 t2.setText(stxt2);
				 
				BROWSER2text2.setText(Genpath);
				BROWSER2text1.setText(uploadpath);
				
				
				download = (Button) layout.findViewById(R.id.genareate);
				View btnClosePopup2 = (Button) layout.findViewById(R.id.btn_close_popup);
				savepop = (Button) layout.findViewById(R.id.save);
				browsepop = (Button) layout.findViewById(R.id.browse);
				uploadpop = (Button) layout.findViewById(R.id.upload);
				Button BROWSER2pop = (Button) layout.findViewById(R.id.BROWSER2);
				Button genareatepop = (Button) layout.findViewById(R.id.genareate);
				updatestatuspop = (Button) layout.findViewById(R.id.updatestatus);
				createbillingpop = (Button) layout.findViewById(R.id.createbilling);
				alternativepop = (Button) layout.findViewById(R.id.alternative);
				//Button cancelpop = (Button) layout.findViewById(R.id.cancel);
				spinner_status = (Spinner) layout.findViewById(R.id.status);
				btnClosePopup2.setOnClickListener(cancel_button_click_listener2);
				btnbrows2=(Button) layout.findViewById(R.id.BROWSER2);
				
				download.setOnClickListener(this);
				btnbrows2.setOnClickListener(this);
				browsepop.setOnClickListener(this);
				uploadpop.setOnClickListener(this);
				savepop.setOnClickListener(this);
				//spinner_status.setOnItemSelectedListener(this);
				updatestatuspop.setOnClickListener(this);
				createbillingpop.setOnClickListener(this);
				alternativepop.setOnClickListener(this);
				
				spinner_status.setOnItemSelectedListener(new OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
						ID = (TextView) view.findViewById(R.id.Id);
						statusid = ID.getText().toString();
						TEXT = (TextView) view.findViewById(R.id.Name);
						statusval = TEXT.getText().toString();

						// Showing selected spinner item
						//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
				
				try {
					spinner_statusdata();
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				
				savepop.setEnabled(false);
				browsepop.setEnabled(false);
				uploadpop.setEnabled(false);
				BROWSER2pop.setEnabled(false);
				genareatepop.setEnabled(false);
				updatestatuspop.setEnabled(false);
				spinner_status.setEnabled(false);
				createbillingpop.setEnabled(false);
				alternativepop.setEnabled(false);
				//cancelpop.setEnabled(false); 	
				
				savepop.setClickable(false);
				browsepop.setClickable(false);
				uploadpop.setClickable(false);
				spinner_status.setClickable(false);
				BROWSER2pop.setClickable(false);
				genareatepop.setClickable(false);
				updatestatuspop.setClickable(false);
				createbillingpop.setClickable(false);
				alternativepop.setClickable(false);
				//cancelpop.setClickable(false);
				
				
				System.out.println(TrnspNamepop);
				
				if(TrnspNamepop.equals("ADD-PO-D") || TrnspNamepop.equals("ADD-PO"))
				{
					savepop.setEnabled(true);
					browsepop.setEnabled(true);
					uploadpop.setEnabled(true);
					BROWSER2pop.setEnabled(false);
					genareatepop.setEnabled(false);
					updatestatuspop.setEnabled(false);
					spinner_status.setEnabled(false);
					createbillingpop.setEnabled(true);
					alternativepop.setEnabled(true);
					//cancelpop.setEnabled(true);
					
					savepop.setClickable(true);
					browsepop.setClickable(true);
					uploadpop.setClickable(true);
					BROWSER2pop.setClickable(false);
					genareatepop.setClickable(false);
					updatestatuspop.setClickable(false);
					spinner_status.setClickable(false);
					createbillingpop.setClickable(true);
					alternativepop.setClickable(true);
					//cancelpop.setClickable(true);
				}
				if(TrnspNamepop.equals("GDOC"))
				{
					savepop.setEnabled(false);
					browsepop.setEnabled(false);
					uploadpop.setEnabled(false);
					BROWSER2pop.setEnabled(true);
					genareatepop.setEnabled(true);
					updatestatuspop.setEnabled(false);
					spinner_status.setEnabled(false);
					createbillingpop.setEnabled(false);
					alternativepop.setEnabled(true);
					//cancelpop.setEnabled(true);
					
					savepop.setClickable(false);
					browsepop.setClickable(false);
					uploadpop.setClickable(false);
					BROWSER2pop.setClickable(true);
					genareatepop.setClickable(true);
					updatestatuspop.setClickable(false);
					spinner_status.setClickable(false);
					createbillingpop.setClickable(false);
					alternativepop.setClickable(true);
					//cancelpop.setClickable(true);
				}
				if(TrnspNamepop.equals("SDOC"))
				{
					savepop.setEnabled(true);
					browsepop.setEnabled(true);
					uploadpop.setEnabled(true);
					BROWSER2pop.setEnabled(false);
					genareatepop.setEnabled(false);
					updatestatuspop.setEnabled(false);
					spinner_status.setEnabled(false);
					createbillingpop.setEnabled(false);
					alternativepop.setEnabled(true);
					//cancelpop.setEnabled(true);
					
					savepop.setClickable(true);
					browsepop.setClickable(true);
					uploadpop.setClickable(true);
					BROWSER2pop.setClickable(false);
					genareatepop.setClickable(false);
					updatestatuspop.setClickable(false);
					spinner_status.setEnabled(false);
					createbillingpop.setClickable(false);
					alternativepop.setClickable(true);
					//cancelpop.setClickable(true);
				}
				
				if(TrnspNamepop.equals("OPDOC"))
				{
					savepop.setEnabled(false);
					browsepop.setEnabled(true);
					uploadpop.setEnabled(true);
					BROWSER2pop.setEnabled(false);
					genareatepop.setEnabled(false);
					updatestatuspop.setEnabled(false);
					spinner_status.setEnabled(false);
					createbillingpop.setEnabled(false);
					alternativepop.setEnabled(true);
					//cancelpop.setEnabled(true);
					
					savepop.setClickable(false);
					browsepop.setClickable(true);
					uploadpop.setClickable(true);
					BROWSER2pop.setClickable(false);
					genareatepop.setClickable(false);
					updatestatuspop.setClickable(false);
					spinner_status.setClickable(false);
					createbillingpop.setClickable(false);
					alternativepop.setClickable(true);
					//cancelpop.setClickable(true);
				}
				
				if(TrnspNamepop.equals("TDOC"))
				{
					savepop.setEnabled(true);
					browsepop.setEnabled(true);
					uploadpop.setEnabled(true);
					BROWSER2pop.setEnabled(false);
					genareatepop.setEnabled(false);
					updatestatuspop.setEnabled(false);
					spinner_status.setEnabled(false);
					createbillingpop.setEnabled(false);
					alternativepop.setEnabled(true);
					//cancelpop.setEnabled(true);
					
					savepop.setClickable(true);
					browsepop.setClickable(true);
					uploadpop.setClickable(true);
					BROWSER2pop.setClickable(false);
					genareatepop.setClickable(false);
					updatestatuspop.setClickable(false);
					spinner_status.setClickable(false);
					createbillingpop.setClickable(false);
					alternativepop.setClickable(true);
					//cancelpop.setClickable(true);
				}
				if(TrnspNamepop.equals("FWDOC"))
				{
					savepop.setEnabled(true);
					browsepop.setEnabled(false);
					uploadpop.setEnabled(false);
					BROWSER2pop.setEnabled(false);
					genareatepop.setEnabled(false);
					updatestatuspop.setEnabled(true);
					spinner_status.setEnabled(true);
					createbillingpop.setEnabled(false);
					alternativepop.setEnabled(true);
					//cancelpop.setEnabled(true);
					
					savepop.setClickable(true);
					browsepop.setClickable(false);
					uploadpop.setClickable(false);
					BROWSER2pop.setClickable(false);
					genareatepop.setClickable(false);
					updatestatuspop.setClickable(true);
					spinner_status.setClickable(true);
					createbillingpop.setClickable(false);
					alternativepop.setClickable(true);
					//cancelpop.setClickable(true);
				}
				if(TrnspNamepop.equals("MANUAL-FIN") || TrnspNamepop.equals("MANUAL-FIN-C") || TrnspNamepop.equals("MANUAL-IC"))
				{
					savepop.setEnabled(true);
					browsepop.setEnabled(false);
					uploadpop.setEnabled(false);
					BROWSER2pop.setEnabled(false);
					genareatepop.setEnabled(false);
					updatestatuspop.setEnabled(true);
					spinner_status.setEnabled(true);
					createbillingpop.setEnabled(false);
					alternativepop.setEnabled(true);
					//cancelpop.setEnabled(true);
					
					savepop.setClickable(true);
					browsepop.setClickable(false);
					uploadpop.setClickable(false);
					BROWSER2pop.setClickable(false);
					genareatepop.setClickable(false);
					updatestatuspop.setClickable(true);
					spinner_status.setClickable(true);
					createbillingpop.setClickable(false);
					alternativepop.setClickable(true);
					//cancelpop.setClickable(true);
				}
				if(TrnspNamepop.equals("FEES"))
				{
					savepop.setEnabled(true);
					browsepop.setEnabled(false);
					uploadpop.setEnabled(false);
					BROWSER2pop.setEnabled(false);
					genareatepop.setEnabled(false);
					updatestatuspop.setEnabled(false);
					spinner_status.setEnabled(false);
					createbillingpop.setEnabled(true);
					alternativepop.setEnabled(true);
					//cancelpop.setEnabled(true);
					
					savepop.setClickable(true);
					browsepop.setClickable(false);
					uploadpop.setClickable(false);
					BROWSER2pop.setClickable(false);
					genareatepop.setClickable(false);
					updatestatuspop.setClickable(false);
					spinner_status.setClickable(false);
					createbillingpop.setClickable(true);
					alternativepop.setClickable(true);
					//cancelpop.setClickable(true);
				}
				
				System.out.println(sUserRole);
				if(sUserRole.equals("MG") || sUserRole.equals("LA"))
				{
					System.out.println("Enabled");
					updatestatuspop.setClickable(true);
					spinner_status.setClickable(true);
					updatestatuspop.setEnabled(true);
					spinner_status.setEnabled(true);
				}

			
				
	}
	
	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		if (v == add_optional) {
			initpopforoptions();
		}
		if (v == process_step) {
			initpopforsteps();
		}
		if (v == view_file) {
			System.out.println("btn view clicked ");
			viewfile();
		}
		
		if (v == details_btn) {
			 Intent to_purchaser = new Intent(ProcessCaseProcessTab.this, ProcessCaseDetails.class);
	           startActivity(to_purchaser);
		}
		if (v == purchaser_btn) {
	        Intent to_purchaser = new Intent(ProcessCaseProcessTab.this, ProcessCasePurchaser.class);
	        startActivity(to_purchaser);
	    }
	    if (v == vendor_btn) {
	        Intent to_vendor = new Intent(ProcessCaseProcessTab.this, ProcessCaseVendor.class);
	        startActivity(to_vendor);
	    }
	    if (v == property_btn) {
	        Intent to_loan_pricipal = new Intent(ProcessCaseProcessTab.this, ProcessCaseProperty.class);
	        startActivity(to_loan_pricipal);
	    }
	    if (v == loan_principal_btn) {
	        Intent to_loan_pricipal = new Intent(ProcessCaseProcessTab.this, ProcessCaseLoanPrincipal.class);
	        startActivity(to_loan_pricipal);
	    }
	    if (v == loan_subsidary_btn) {
	        Intent to_loan_subsidiary = new Intent(ProcessCaseProcessTab.this, ProcesscaseLoanSubsidiary.class);
	        startActivity(to_loan_subsidiary);
	    }
	    if (v == process_btn) {
	        Intent to_loan_subsidiary = new Intent(ProcessCaseProcessTab.this, ProcessCaseProcessTab.class);
	        startActivity(to_loan_subsidiary);
	    }
	    if(v == walkin)
        {
        	Intent i = new Intent(ProcessCaseProcessTab.this, WalkInActivity.class);
			startActivity(i);
        }
	    if(v == alternativepop)
	    {
	    	alternativepopup();
	    }
	    if(v == updatestatuspop)
        {
	    	if(statusval.toString().equals("-- Select --"))
	    	{
	    		slog("Select Status");
	    	}
	    	else
	    	{
	    		dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
		    	uodatestatus();
	    	}
	    	
        }
	    if(v == createbillingpop)
        {
	    	dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
	    	createbilling();
        }
	    
	    if(v == savepop)
	    {	dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
	    	saveprocess();
	    }
	    if (v == next_sec) {
	    	vnext_sec.setVisibility(View.GONE);
			vopen_sec.setVisibility(View.VISIBLE);
			view_file.setEnabled(true);
			view_file.setClickable(true);
			add_optional.setEnabled(true);
	        try {
	        	dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
				GetNextSection();
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
	    }
	    if (v == open_sec) {
	    	vnext_sec.setVisibility(View.VISIBLE);
			vopen_sec.setVisibility(View.GONE);
			view_file.setEnabled(true);
			view_file.setClickable(true);
			add_optional.setEnabled(true);
	        try {
	        	dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
				opensection();
				dialog.dismiss();
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
	    }
	    
	    if (v == past_sec) {	
	    	vnext_sec.setVisibility(View.GONE);
			vopen_sec.setVisibility(View.VISIBLE);
			view_file.setEnabled(true);
			view_file.setClickable(true);
			add_optional.setEnabled(false);
	        try {
	        	dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Loading data...", true);
				PastSection();
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
	    }
	    
	    if (v == btnbrows2) {	        
	    	Intent intent = new Intent(ProcessCaseProcessTab.this, DirectoryPicker.class);
	    	startActivityForResult(intent, DirectoryPicker.PICK_DIRECTORY);
	    	
	    }
	    
	    if (v == uploadpop) {
	    	if(BROWSER2text1.getText().toString().isEmpty() || BROWSER2text1.getText().toString()=="" || !isFilecheck(BROWSER2text1.getText().toString()))
	    	{
	    		slog("Select File to Upload");
	    	}
	    	else
	    	{
	    		dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Uploading file...", true);
	    		new Thread(new Runnable() {
		            public void run() {              
		            	uploadFile(fileName);
		            	
		            	
		            }
		        }).start();
	    	}
	    }
	    if (v == browsepop) {	        
	    	Intent intent = new Intent(ProcessCaseProcessTab.this, FilePicker.class);
	    	// optionally set options here
	    	startActivityForResult(intent, PICK_FILEUPLOAD);
	    	
	    }
	    
	    if (v == download) {
	    	if(BROWSER2text2.getText().toString().isEmpty() || BROWSER2text2.getText().toString()=="")
	    	{
	    		slog("Select Folder to Download");
	    	}
	    	else
	    	{
	    	dialog = ProgressDialog.show(ProcessCaseProcessTab.this, "", "Downloading file...", true);
	    	getgenfile();
	    	}
	    	
	    	}
	        	
		
	}
	
	public void uodatestatus() {
	
		// Passing value in JSON format in first fields
		JSONObject jsonObject_sv = new JSONObject();

		// jsonObject.put("Category", "SPA");
		
		try {
			
			// Find confirmation message
			// messageResult = jsonObject.getString("Result").toString();
			JSONObject jobsave = new JSONObject (arrayResponse.get(clickedposition).toString());
			jsonObject_sv.put("CaseNo", CASEFILENUMBER);
			jsonObject_sv.put("ItemCode", ItemCoderes);	
			jsonObject_sv.put("DocEntry",  DocEntryres);	
			jsonObject_sv.put("DocNum",  DocNumres);	
			jsonObject_sv.put("LineNum",  LineNumres);
			jsonObject_sv.put("TrnspName",  TrnspNamepop);
			jsonObject_sv.put("UserName", user_name);
			jsonObject_sv.put("Details", jobsave.get("Details").toString());
			jsonObject_sv.put("IntNo", jobsave.get("IntNo").toString());
			jsonObject_sv.put("SortCode", jobsave.get("SortCode").toString());
			jsonObject_sv.put("CreationDate", jobsave.get("CreationDate").toString());
			jsonObject_sv.put("Qty", jobsave.get("Qty").toString());
			jsonObject_sv.put("Price", jobsave.get("Price").toString());
			jsonObject_sv.put("ActionBy", jobsave.get("ActionBy").toString());
			jsonObject_sv.put("Status", statusval);
			jsonObject_sv.put("LastUpdate", jobsave.get("LastUpdate").toString());
			jsonObject_sv.put("StatusDate", jobsave.get("StatusDate").toString());
			jsonObject_sv.put("TrnspName", jobsave.get("TrnspName").toString());
			jsonObject_sv.put("ForwardParty", jobsave.get("ForwardParty").toString());
			jsonObject_sv.put("ItmsGrpNam", jobsave.get("ItmsGrpNam").toString());
			jsonObject_sv.put("UserRole", sUserRole);			
			jsonObject_sv.put("Remarks", Remarks.getText().toString());
			jsonObject_sv.put("UserName", user_name);
			
			

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject_sv.toString());
			System.out.println("params");
			System.out.println(params);

			RestService.post("SPA_ProcessCase_UpdateStatusClick", params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);
					dialog.dismiss();

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Update Btn Confirmed");
					System.out.println(arg2);
					
					try {
						JSONArray arry = new JSONArray(arg2.toString());				
						jsonResponse_sv = arry.getJSONObject(0);
						//setavalubyRC(jsonResponse);
					} catch (JSONException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}

					String StatusResult = null;
					String messageDisplay = null;
					// Find status Response
					try {
						//StatusResult = jsonResponse.getString("Result").toString();
						messageDisplay = jsonResponse_sv.getString("DisplayMessage").toString();
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					
						 
							Toast.makeText(ProcessCaseProcessTab.this, messageDisplay, Toast.LENGTH_LONG).show();
							dialog.dismiss();
							finish();
							startActivity(getIntent());
							
						
							

					
					
				}

				
				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse_sv = new JSONArray(arg0);
					jsonResponse_sv = arrayResponse_sv.getJSONObject(0);

					System.out.println("Document Details ParseResponse");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public void createbilling() {
	
		// Passing value in JSON format in first fields
		JSONObject jsonObject_sv = new JSONObject();

		// jsonObject.put("Category", "SPA");
		
		try {
			
			// Find confirmation message
			// messageResult = jsonObject.getString("Result").toString();
			JSONObject jobsave = new JSONObject (arrayResponse.get(clickedposition).toString());
			jsonObject_sv.put("CaseNo", CASEFILENUMBER);
			jsonObject_sv.put("ItemCode", ItemCoderes);	
			jsonObject_sv.put("DocEntry",  DocEntryres);	
			jsonObject_sv.put("DocNum",  DocNumres);	
			jsonObject_sv.put("LineNum",  LineNumres);
			jsonObject_sv.put("TrnspName",  TrnspNamepop);
			jsonObject_sv.put("UserName", user_name);
			jsonObject_sv.put("Details", jobsave.get("Details").toString());
			jsonObject_sv.put("IntNo", jobsave.get("IntNo").toString());
			jsonObject_sv.put("SortCode", jobsave.get("SortCode").toString());
			jsonObject_sv.put("CreationDate", jobsave.get("CreationDate").toString());
			jsonObject_sv.put("Qty", jobsave.get("Qty").toString());
			jsonObject_sv.put("Price", jobsave.get("Price").toString());
			jsonObject_sv.put("ActionBy", jobsave.get("ActionBy").toString());
			jsonObject_sv.put("Status", jobsave.get("Status").toString());
			jsonObject_sv.put("LastUpdate", jobsave.get("LastUpdate").toString());
			jsonObject_sv.put("StatusDate", jobsave.get("StatusDate").toString());
			jsonObject_sv.put("TrnspName", jobsave.get("TrnspName").toString());
			jsonObject_sv.put("ForwardParty", jobsave.get("ForwardParty").toString());
			jsonObject_sv.put("ItmsGrpNam", jobsave.get("ItmsGrpNam").toString());
			jsonObject_sv.put("UserRole", sUserRole);			
			jsonObject_sv.put("Remarks", Remarks.getText().toString());
			jsonObject_sv.put("UserName", user_name);
			
			

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject_sv.toString());
			System.out.println("params");
			System.out.println(params);

			RestService.post("SPA_ProcessCase_CreateBillingClick", params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);
					dialog.dismiss();

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Billing Btn Confirmed");
					System.out.println(arg2);
					
					try {
						JSONArray arry = new JSONArray(arg2.toString());				
						jsonResponse_sv = arry.getJSONObject(0);
						//setavalubyRC(jsonResponse);
					} catch (JSONException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}

					String StatusResult = null;
					String messageDisplay = null;
					// Find status Response
					try {
						//StatusResult = jsonResponse.getString("Result").toString();
						messageDisplay = jsonResponse_sv.getString("DisplayMessage").toString();
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					
						 
							Toast.makeText(ProcessCaseProcessTab.this, messageDisplay, Toast.LENGTH_LONG).show();
							dialog.dismiss();
							finish();
							startActivity(getIntent());
							
						
							

					
					
				}

				
				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse_sv = new JSONArray(arg0);
					jsonResponse_sv = arrayResponse_sv.getJSONObject(0);

					System.out.println("Document Details ParseResponse");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public void saveprocess() {
	
		// Passing value in JSON format in first fields
		JSONObject jsonObject_sv = new JSONObject();

		// jsonObject.put("Category", "SPA");
		
		try {
			
			System.out.println(arrayResponse.get(clickedposition).toString());
			// Find confirmation message
			// messageResult = jsonObject.getString("Result").toString();
			JSONObject jobsave = new JSONObject (arrayResponse.get(clickedposition).toString());
			jsonObject_sv.put("CaseNo", CASEFILENUMBER);
			jsonObject_sv.put("ItemCode", ItemCoderes);	
			jsonObject_sv.put("DocEntry",  DocEntryres);	
			jsonObject_sv.put("DocNum",  DocNumres);	
			jsonObject_sv.put("LineNum",  LineNumres);
			jsonObject_sv.put("TrnspName",  TrnspNamepop);
			jsonObject_sv.put("UserName", user_name);
			jsonObject_sv.put("Details", jobsave.get("Details").toString());
			jsonObject_sv.put("IntNo", jobsave.get("IntNo").toString());
			jsonObject_sv.put("SortCode", jobsave.get("SortCode").toString());
			jsonObject_sv.put("CreationDate", jobsave.get("CreationDate").toString());
			jsonObject_sv.put("Qty", jobsave.get("Qty").toString());
			jsonObject_sv.put("Price", jobsave.get("Price").toString());
			jsonObject_sv.put("ActionBy", jobsave.get("ActionBy").toString());
			jsonObject_sv.put("Status", jobsave.get("Status").toString());
			jsonObject_sv.put("LastUpdate", jobsave.get("LastUpdate").toString());
			jsonObject_sv.put("StatusDate", jobsave.get("StatusDate").toString());
			jsonObject_sv.put("TrnspName", jobsave.get("TrnspName").toString());
			jsonObject_sv.put("ForwardParty", jobsave.get("ForwardParty").toString());
			jsonObject_sv.put("ItmsGrpNam", jobsave.get("ItmsGrpNam").toString());
			jsonObject_sv.put("UserRole", sUserRole);			
			jsonObject_sv.put("Remarks", Remarks.getText().toString());
			jsonObject_sv.put("UserName", user_name);
			
			

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject_sv.toString());
			System.out.println("params");
			System.out.println(params);

			RestService.post("SPA_ProcessCase_SaveClick", params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);
					dialog.dismiss();

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Save Btn Confirmed");
					System.out.println(arg2);
					
					try {
						JSONArray arry = new JSONArray(arg2.toString());				
						jsonResponse_sv = arry.getJSONObject(0);
						//setavalubyRC(jsonResponse);
					} catch (JSONException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}

					String StatusResult = null;
					String messageDisplay = null;
					// Find status Response
					try {
						//StatusResult = jsonResponse.getString("Result").toString();
						messageDisplay = jsonResponse_sv.getString("DisplayMessage").toString();
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					
						 
							Toast.makeText(ProcessCaseProcessTab.this, messageDisplay, Toast.LENGTH_LONG).show();
							dialog.dismiss();
							finish();
							startActivity(getIntent());
							
						
							

					
					
				}

				
				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse_sv = new JSONArray(arg0);
					jsonResponse_sv = arrayResponse_sv.getJSONObject(0);

					System.out.println("Document Details ParseResponse");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	
	private static void setNewFolderEnabled(boolean m_newFolderEnabled) {
		// TODO Auto-generated method stub
		
	}

	private static void chooseDirectory(String m_chosenDir) {
		// TODO Auto-generated method stub
		
	}

	public int uploadFile(String sourceFileUri) {

        String fileName = sourceFileUri;

        HttpURLConnection conn = null;
        DataOutputStream dos = null;  
        String lineEnd = "\r\n";
        String twoHyphens = "--";
        String boundary = "*****";
        int bytesRead, bytesAvailable, bufferSize;
        byte[] buffer;
        int maxBufferSize = 1 * 1024 * 1024; 
        File sourceFile = new File(sourceFileUri); 

        if (!sourceFile.isFile()) {
            dialog.dismiss(); 
            Log.e("uploadFile", "Source File not exist :" +fileName);
            return 0;
        }
        else
        {
            try { 

                // open a URL connection to the Servlet
                FileInputStream fileInputStream = new FileInputStream(sourceFile);
                URL url = new URL(METHOD_ADDFILE);

                // Open a HTTP  connection to  the URL
                conn = (HttpURLConnection) url.openConnection(); 
                conn.setDoInput(true); // Allow Inputs
                conn.setDoOutput(true); // Allow Outputs
                conn.setUseCaches(false); // Don't use a Cached Copy
                conn.setRequestMethod("POST");
                conn.setRequestProperty("Connection", "Keep-Alive");
                conn.setRequestProperty("ENCTYPE", "multipart/form-data");
                conn.setRequestProperty("Content-Type", "multipart/form-data;boundary=" + boundary);
                conn.setRequestProperty("uploaded_file", fileName); 

                dos = new DataOutputStream(conn.getOutputStream());

                dos.writeBytes(twoHyphens + boundary + lineEnd); 
                dos.writeBytes("Content-Disposition: form-data; name=\"uploaded_file\";filename="+ fileName + "" + lineEnd);
                dos.writeBytes(lineEnd);

                // create a buffer of  maximum size
                bytesAvailable = fileInputStream.available(); 

                bufferSize = Math.min(bytesAvailable, maxBufferSize);
                buffer = new byte[bufferSize];

                // read file and write it into form...
                bytesRead = fileInputStream.read(buffer, 0, bufferSize);  

                while (bytesRead > 0) {

                    dos.write(buffer, 0, bufferSize);
                    bytesAvailable = fileInputStream.available();
                    bufferSize = Math.min(bytesAvailable, maxBufferSize);
                    bytesRead = fileInputStream.read(buffer, 0, bufferSize);   

                }

                // send multipart form data necesssary after file data...
                dos.writeBytes(lineEnd);
                dos.writeBytes(twoHyphens + boundary + twoHyphens + lineEnd);

                // Responses from the server (code and message)
                serverResponseCode = conn.getResponseCode();
                //serverResponseCode = conn.getContent();
               // String serverResponseMessage = conn.getResponseMessage();
                String serverResponseMessage = conn.getContentEncoding();

                Log.i("uploadFile", "HTTP Response is 1 : "+ serverResponseMessage + ": " + serverResponseCode);
                
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                StringBuilder sb = new StringBuilder();
                String line;
                while ((line = br.readLine()) != null) {
                    sb.append(line+"\n");
                }
                br.close();
                JSONArray arry = new JSONArray(sb.toString());
                System.out.println(arry);
                JSONObject RESULT = arry.getJSONObject(0);
                FILEUPLOADRESULT = RESULT.get("Result").toString(); 

                if(serverResponseCode == 200){

                    runOnUiThread(new Runnable() {
                        public void run() {
                 
                            addCaseDocumentToRead();
                        }
                    });
                    
                }    

                //close the streams //
                fileInputStream.close();
                dos.flush();
                dos.close();
                
                
                
                
                
                

            } catch (MalformedURLException ex) {

                dialog.dismiss();  
                ex.printStackTrace();

                runOnUiThread(new Runnable() {
                    public void run() {

                        Toast.makeText(ProcessCaseProcessTab.this, "MalformedURLException", 
                                Toast.LENGTH_LONG).show();
                    }
                });

                Log.e("Upload file to server", "error: " + ex.getMessage(), ex);  
            } catch (Exception e) {

                dialog.dismiss();  
                e.printStackTrace();

                runOnUiThread(new Runnable() {
                    public void run() {

                        Toast.makeText(ProcessCaseProcessTab.this, "Got Exception : see logcat ", 
                                Toast.LENGTH_LONG).show();
                    }
                });
                Log.e("Upload file to server Exception", "Exception : "
                        + e.getMessage(), e);  
            }
            //dialog.dismiss();       
            return serverResponseCode; 

        } // End else block 
    } 
	
	//get gen file from server 
	public void  getgenfile()
	{
		
		

		RequestParams params = null;
		params = new RequestParams();

		JSONObject gen_jsonObject = new JSONObject();
		
		try {
			gen_jsonObject.put("CaseNo", CASEFILENUMBER);
			gen_jsonObject.put("ItemCode", ItemCoderes);	
			gen_jsonObject.put("DocEntry",  DocEntryres);	
			gen_jsonObject.put("DocNum",  DocNumres);	
			gen_jsonObject.put("LineNum",  LineNumres);
			gen_jsonObject.put("TrnspName",  TrnspNamepop);
			gen_jsonObject.put("UserName", user_name);
			
			
			params.put("sJsonInput", gen_jsonObject.toString());
			
		} catch (JSONException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		
		System.out.println(gen_jsonObject.toString());

		RestService.post("SPA_ProcessCase_GenerateClick", params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");
						dialog.dismiss();
					}

					@Override
					protected String parseResponse(String s, boolean b)
							throws Throwable {
						return null;
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out.println("GenFile Success Details ");
						System.out.println(arg2);

						try {
							
							
							

							JSONArray arrayResponse_gen = new JSONArray(arg2);							
							
							

								jsonResponse_gen = arrayResponse_gen.getJSONObject(0);							
								
								Thread thread = new Thread(new Runnable(){
						        	@Override
						        	public void run() {
						        	    try {
						        	    	String filelocation = jsonResponse_gen.getString("sFileLocation").toString();				
						    				String msg = jsonResponse_gen.getString("sDisplayMessage").toString();
						    				String results = jsonResponse_gen.getString("sResult").toString();
						    				if(results.equals("FAILURE"))
						    						{
						    							Toast.makeText(ProcessCaseProcessTab.this, msg,
						    							Toast.LENGTH_SHORT).show();
						    							finish();
						    							startActivity(getIntent());
						    						}
						    				else if(FILESAVELOCATION=="")
						    				{
						    					Toast.makeText(ProcessCaseProcessTab.this, "Select Folder Path",
						    					Toast.LENGTH_SHORT).show();
						    					finish();
												startActivity(getIntent());
						    				}
						    				else
						    				{	    						
						    				
						        	    	String fileURL = "http://54.251.51.69:3878"+filelocation;
						        	        String saveDir = FILESAVELOCATION;
						        	    	resultofdownload = downloadFile(fileURL, saveDir);
						        	    	finish();
											startActivity(getIntent());
						    				}
						        	    } catch (Exception e) {
						        	    	resultofdownload = "File Not Downloading..";
						        	    	dialog.dismiss();
						        	    	finish();
											startActivity(getIntent());
						        	       Log.e("ddd", e.getMessage());
						        	       //slog("File Not Downloading..");
						        	    }
						        	}
						        	});
						        	thread.start();
						        	
						        	
						        	
						        	
												

						} catch (JSONException e) {

							e.printStackTrace();
							dialog.dismiss();
						}
					}
				});
		
			//return true;
		
		slog(resultofdownload);
		pwindo2.dismiss();		
		
		
	}
	
	public void slog(String msg)
	{
	 Toast.makeText(ProcessCaseProcessTab.this, msg,
				Toast.LENGTH_SHORT).show();	
	}
	//filedownload
	 public String downloadFile(String fileURL, String saveDir)
	            throws IOException {
	        URL url = new URL(fileURL);
	        System.out.print("okthommmm");
	        HttpURLConnection httpConn = (HttpURLConnection) url.openConnection();
	        int responseCode = httpConn.getResponseCode();
	 
	        // always check HTTP response code first
	        if (responseCode == HttpURLConnection.HTTP_OK) {
	            String fileName = "";
	            String disposition = httpConn.getHeaderField("Content-Disposition");
	            String contentType = httpConn.getContentType();
	            int contentLength = httpConn.getContentLength();
	 
	            if (disposition != null) {
	                // extracts file name from header field
	                int index = disposition.indexOf("filename=");
	                if (index > 0) {
	                    fileName = disposition.substring(index + 10,
	                            disposition.length() - 1);
	                }
	            } else {
	                // extracts file name from URL
	                fileName = fileURL.substring(fileURL.lastIndexOf("/") + 1,
	                        fileURL.length());
	            }
	 
	            System.out.println("Content-Type = " + contentType);
	            System.out.println("Content-Disposition = " + disposition);
	            System.out.println("Content-Length = " + contentLength);
	            System.out.println("fileName = " + fileName);
	 
	            // opens input stream from the HTTP connection
	            InputStream inputStream = httpConn.getInputStream();
	            String saveFilePath = saveDir + File.separator + fileName;
	             
	            // opens an output stream to save into file
	            FileOutputStream outputStream = new FileOutputStream(saveFilePath);
	 
	            int bytesRead = -1;
	            byte[] buffer = new byte[BUFFER_SIZE];
	            while ((bytesRead = inputStream.read(buffer)) != -1) {
	                outputStream.write(buffer, 0, bytesRead);
	            }
	 
	            outputStream.close();
	            inputStream.close();
	 
	            System.out.println("File downloaded");
	            dialog.dismiss();
	           	rtext =  "File Generated and Saved Succesfully";
	                  
	        } else {
	            System.out.println("No file to download. Server replied HTTP code: " + responseCode);
	            dialog.dismiss();
	            rtext = "No file to download";     
	            
	        }
	        
	        httpConn.disconnect();
	        return rtext;
	    }
	
	 
	public boolean dispatchTouchEvent(MotionEvent ev) {
	InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
	imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
	return super.dispatchTouchEvent(ev);

}
	
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if(requestCode == DirectoryPicker.PICK_DIRECTORY && resultCode == RESULT_OK) {
			Bundle extras = data.getExtras();
			String path = (String) extras.get(DirectoryPicker.CHOSEN_DIRECTORY);
			// do stuff with path
			FILESAVELOCATION = path;
			BROWSER2text2.setText(FILESAVELOCATION);
			System.out.println(path);
		}
		if(requestCode == PICK_FILEUPLOAD && resultCode == RESULT_OK) {
			if (data.hasExtra(FilePicker.EXTRA_FILE_PATH)) {

				selectedFile = new File(data.getStringExtra(FilePicker.EXTRA_FILE_PATH));
				fileName = selectedFile.getPath();
				BROWSER2text1.setText(fileName);
				
				//dialog = ProgressDialog.show(AddCaseStep3of4.this, "", "Uploading file...", true);
				
				
			}
		}
		
		
	}
	
	
	
	public void addCaseDocumentToRead() {
		
		
		// File from File Path
		String file = fileName.substring(fileName.lastIndexOf('/') + 1);
		System.out.println(file);
		System.out.println("ok add case");

		// Passing value in JSON format in first fields
		JSONObject jsonObject_up = new JSONObject();

		// jsonObject.put("Category", "SPA");
		try {
			
			// Find confirmation message
			// messageResult = jsonObject.getString("Result").toString();
			
			jsonObject_up.put("CaseNo", CASEFILENUMBER);
			jsonObject_up.put("ItemCode", ItemCoderes);	
			jsonObject_up.put("DocEntry",  DocEntryres);	
			jsonObject_up.put("DocNum",  DocNumres);	
			jsonObject_up.put("LineNum",  LineNumres);
			jsonObject_up.put("TrnspName",  TrnspNamepop);
			jsonObject_up.put("UserName", user_name);
			jsonObject_up.put("FileName", FILEUPLOADRESULT);

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject_up.toString());
			System.out.println("params");
			System.out.println(params);

			RestService.post("SPA_ProcessCase_UploadClick", params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);
					dialog.dismiss();

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Document Send  Confirmed");
					System.out.println(arg2);
					
					try {
						JSONArray arry = new JSONArray(arg2.toString());				
						jsonResponse_up = arry.getJSONObject(0);
						//setavalubyRC(jsonResponse);
					} catch (JSONException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}

					String StatusResult = null;
					String messageDisplay = null;
					// Find status Response
					try {
						//StatusResult = jsonResponse.getString("Result").toString();
						messageDisplay = jsonResponse_up.getString("DisplayMessage").toString();
						Toast.makeText(ProcessCaseProcessTab.this, messageDisplay, Toast.LENGTH_LONG).show();
						dialog.dismiss();
						finish();
						startActivity(getIntent());
						
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
						dialog.dismiss();
						finish();
						startActivity(getIntent());
					}
					
						 
							
							
						
							

					
					
				}

				
				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse_up = new JSONArray(arg0);
					jsonResponse_up = arrayResponse_up.getJSONObject(0);

					System.out.println("Document Details ParseResponse");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public boolean  isFilecheck(String path) {
		File file = new File(path);
		return file.isFile();
	}
	
	private OnClickListener cancel_button_click_listener = new OnClickListener() {
		public void onClick(View v) {
		pwindo.dismiss();
		pwindo.isOutsideTouchable();

		}
		};
		private OnClickListener cancel_button_click_listener1 = new OnClickListener() {
			public void onClick(View v) {
			pwindo1.dismiss();
			pwindo1.isOutsideTouchable();

			}
			};
			private OnClickListener cancel_button_click_listener2 = new OnClickListener() {
				public void onClick(View v) {
				pwindo2.dismiss();
				pwindo2.isOutsideTouchable();

				}
				};
	}
