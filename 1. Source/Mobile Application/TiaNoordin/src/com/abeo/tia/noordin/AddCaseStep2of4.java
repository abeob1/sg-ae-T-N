package com.abeo.tia.noordin;

import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;

import net.sf.andpdf.pdfviewer.PdfViewerActivity;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.app.ActionBar.LayoutParams;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.CursorLoader;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.PopupWindow;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ZoomButton;
import android.widget.AdapterView.OnItemSelectedListener;

public class AddCaseStep2of4 extends BaseActivity   implements OnClickListener {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_AddCase_ListOfItems
	// Find list of Add Case Item list web method
	private final String METHOD_ADDCASE_ITEMLIST = "SPA_AddCase_ListOfItems";
	private final String METHOD_PROPERTY_LIST_DROPDOWN = "SPA_GetProject";
	private final String METHOD_PROPERTY_BDS_DROPDOWN = "SPA_Property_GetDropdownValues";
	private final String METHOD_ADD_CASE2 = "SPA_AddCase_AddPropery";
	
	
	String codeDetailResponse = "", titleTypeDetailResponse = "", titleNoDetailResponse = "",
			lotTypeDetailResponse = "", lotNoDetailResponse = "", formerlyDetailResponse = "", bpmDetailResponse = "",
			stateDetailResponse = "", areaDetailResponse = "", lotAreaDetailResponse = "",
			lotaresSoftDetailResponse = "", lastupDateDetailResponse = "", developerCodeResponse = "",
			developerDetailResponse = "", projectDetailResponse = "", DevlicNoDetailResponse = "",
			devSolictorCodeResponse = "", devSolictorDetailResponse = "", devSolictorLocDetailResponse = "",
			bankCodeResponse = "", bankDetailResponse = "", branchDetailResponse = "", panNoDetailResponse = "",
			prsentDetailResponse = "";
	

	EditText edittextFile;
	Button buttonOk, buttonconfirm,btnClosePopup;
	ZoomButton btnpdf1, btnpdf2;
	ListView listViewItem;

	// Find Case list items
	String AddCaseListItemNo_detail = "", AddCaseList_ItemName_detail = "";

	TextView messageText,ID, TEXT;
	ProgressDialog dialog = null;
	
	
	
	// Get Project value from api
		private final String TitleType_DROPDOWN = "SPA_GetValidValues";
		
		
		//strings
		String titleValue_id = "", ProjectValue_id = "", bankValue_id = "", developerValue_id = "", solicitorValue_id = "",
				titleValue = "", ProjectValue = "", bankValue = "", developerValue = "", solicitorValue = "";
		String messageDisplay = "", StatusResult = "";
		
		
	//spenner adaptor
		ArrayList<HashMap<String, String>> jsonlistProject = null, jsonlistProjectTitle = null, jsonlistBank = null,
				jsonlistDeveloper = null, jsonlistSolicitor = null;
		String id, name, id_b, name_b, id_d, name_d, id_s, name_s;
		SimpleAdapter sAdap = null, sAdapTYPE = null, sAdapPROJ = null, sAdapBANK = null, sAdapDEV = null,
				sAdapSOLIC = null;
	// Find spinner fields
		Spinner spinnerpropertyTitleType, spinnerpropertyPROJECT, spinnerpropertyLSTCHG_BANKNAME, spinnerpropertyDEVELOPER,
				spinnerpropertyDEVSOLICTOR;
		
		EditText Title , LotType , LotNo , Knownas , Pekan ,Daerah ,Nageri,LotArea ,LastUpdate ,DevLicense ,DevSolictor ,SolicitorLoc ,
		Branch , PAName ,PresentaionNo ;
		Button buttonChooseDoc, buttonFileBrowser, buttonconfirm2,btnfind;
		
		

	/********** File Path *************/
	int serverResponseCode = 0;
	String upLoadServerUri = null;
	String selectedImagePath;
	final String uploadFilePath = Environment.getExternalStorageDirectory().getPath();
	final String uploadFileName = "/go.png";
	//selectedImagePath = "uploadFilePath"+"uploadFileName";

	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;
	ArrayList<HashMap<String, String>> jsonItemList = null;
	String itemCode = "", itemName = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_addcase_step2);
		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);
		Toast.makeText(AddCaseStep2of4.this, "reachec in AddCaseStep1of 4", Toast.LENGTH_SHORT).show();
		
		// Find the SharedPreferences Firstname
				SharedPreferences FirstName = getSharedPreferences("FirstName", Context.MODE_PRIVATE);		
				String FirName = FirstName.getString("FirstName", "");
				TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
				welcome.setText("Welcome "+FirName);

		// Find Mesaage Text and Edit field by Id
		messageText = (TextView) findViewById(R.id.messageText);
		edittextFile = (EditText) findViewById(R.id.editText_AddCaseStep1Browse);

		// Find By Id spinner Address To Use
				spinnerpropertyTitleType = (Spinner) findViewById(R.id.TitleType);
				spinnerpropertyPROJECT = (Spinner) findViewById(R.id.projectsp);
				spinnerpropertyLSTCHG_BANKNAME = (Spinner) findViewById(R.id.banksp);
				spinnerpropertyDEVELOPER = (Spinner) findViewById(R.id.developersp);
				spinnerpropertyDEVSOLICTOR = (Spinner) findViewById(R.id.DevSolisp);
				
				

				// Spinner click listener
				spinnerpropertyTitleType.setOnItemSelectedListener(new OnItemSelectedListener() {

							@Override
							public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
								ID = (TextView) view.findViewById(R.id.Id);
								titleValue_id = ID.getText().toString();
								TEXT = (TextView) view.findViewById(R.id.Name);
								titleValue = TEXT.getText().toString();

								// Showing selected spinner item
								//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

							}

							@Override
							public void onNothingSelected(AdapterView<?> parent) {
								// TODO Auto-generated method stub

							}
						});
				// Spinner click listener
				

				// Spinner click listener
				spinnerpropertyPROJECT.setOnItemSelectedListener(new OnItemSelectedListener() {

							@Override
							public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
								ID = (TextView) view.findViewById(R.id.Id);
								ProjectValue_id = ID.getText().toString();
								TEXT = (TextView) view.findViewById(R.id.Name);
								ProjectValue = TEXT.getText().toString();

								// Showing selected spinner item
								//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

							}

							@Override
							public void onNothingSelected(AdapterView<?> parent) {
								// TODO Auto-generated method stub

							}
						});
				// Spinner click listener
				

				// Spinner click listener
				spinnerpropertyLSTCHG_BANKNAME.setOnItemSelectedListener(new OnItemSelectedListener() {

							@Override
							public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
								ID = (TextView) view.findViewById(R.id.Id);
								bankValue_id = ID.getText().toString();
								TEXT = (TextView) view.findViewById(R.id.Name);
								bankValue = TEXT.getText().toString();

								// Showing selected spinner item
								//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

							}

							@Override
							public void onNothingSelected(AdapterView<?> parent) {
								// TODO Auto-generated method stub

							}
						});
				// Spinner click listener
				

				// Spinner click listener
				spinnerpropertyDEVELOPER.setOnItemSelectedListener(new OnItemSelectedListener() {

							@Override
							public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
								ID = (TextView) view.findViewById(R.id.Id);
								developerValue_id = ID.getText().toString();
								TEXT = (TextView) view.findViewById(R.id.Name);
								developerValue = TEXT.getText().toString();

								// Showing selected spinner item
								//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

							}

							@Override
							public void onNothingSelected(AdapterView<?> parent) {
								// TODO Auto-generated method stub

							}
						});
				// Spinner click listener
				

				// Spinner click listener
				spinnerpropertyDEVSOLICTOR.setOnItemSelectedListener(new OnItemSelectedListener() {

							@Override
							public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
								ID = (TextView) view.findViewById(R.id.Id);
								solicitorValue_id = ID.getText().toString();
								TEXT = (TextView) view.findViewById(R.id.Name);
								solicitorValue = TEXT.getText().toString();

								// Showing selected spinner item
								//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

							}

							@Override
							public void onNothingSelected(AdapterView<?> parent) {
								// TODO Auto-generated method stub

							}
						});
				// Spinner click listener

		// Find button by Id
		btnpdf1 = (ZoomButton) findViewById(R.id.zoomButton_propertyPdf1);
		btnpdf2 = (ZoomButton) findViewById(R.id.zoomButton_propertyPdf2);
		buttonconfirm = (Button) findViewById(R.id.button_PropertyConfirm);
		
		
		// Find EditText Fields
		Title = (EditText) findViewById(R.id.editText_ProperTytitleNo);
		LotType = (EditText) findViewById(R.id.editText_PropertyLottype);
		LotNo = (EditText) findViewById(R.id.editText_PropertyLotPTDNo);
		Knownas = (EditText) findViewById(R.id.editText_PropertyFormerlyKnownAs);
		Pekan = (EditText) findViewById(R.id.editText_PropertyBandarPekanMukin);
		Daerah = (EditText) findViewById(R.id.editText_PropertyDaerahState);
		Nageri = (EditText) findViewById(R.id.editText_PropertyNageriArea);

		LotArea = (EditText) findViewById(R.id.editText_PropertyLotArea);
		LastUpdate = (EditText) findViewById(R.id.editText_PropertyLastUpdateOn);
		DevLicense = (EditText) findViewById(R.id.editText_PropertyDevLicense);
		SolicitorLoc = (EditText) findViewById(R.id.editText_PropertySolicitorLoc);
		Branch = (EditText) findViewById(R.id.editText_PropertyBranch);
		PAName = (EditText) findViewById(R.id.editText_PropertyPAName);
		PresentaionNo = (EditText) findViewById(R.id.editText_PropertyPresentaionNo);
		

		
		btnpdf1.setOnClickListener(this);
		btnpdf2.setOnClickListener(this);
		buttonconfirm.setOnClickListener(this);
		
		Toast.makeText(AddCaseStep2of4.this, " Can't Read The Scanned Files. Kindly Key In the Data.", Toast.LENGTH_SHORT).show();
		
		try {
			dropdownTitle();
			dropdownBankDeveloperSolicitor();
			dropdownPorject();
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	@Override
	public void onClick(View v) {
		if (v == btnpdf1) {
			//initiatePopupWindow();
			
			String pdfurl = "http://54.251.51.69:3878/PDF/MBB.pdf"; //YOUR URL TO PDF
			   String googleDocsUrl = "http://docs.google.com/viewer?url="+ pdfurl;
			   Intent intent = new Intent(Intent.ACTION_VIEW);
			   intent.setDataAndType(Uri.parse(googleDocsUrl ), "text/html");
			   startActivity(intent);
			   
			   
		} else if (v == btnpdf2) {
			 Intent intent = new Intent(this, PdfViewer.class);
		     intent.putExtra(PdfViewerActivity.EXTRA_PDFFILENAME, "http://54.251.51.69:3878/PDF/MBB.pdf");
		     startActivity(intent);
		} else if (v == buttonconfirm) {
			btnconfirm();
		}
	}


	private void btnconfirm() {
		

		JSONObject jsonObject = new JSONObject();
		// jsonObject.put("Category", "SPA");
		try {
			// Find confirmation message
			// messageResult = jsonObject.getString("Result").toString();

			jsonObject.put("CODE", "11");
			jsonObject.put("TITLETYPE", titleValue);
			jsonObject.put("TITLENO", Title.getText().toString());
			jsonObject.put("LOTTYPE", LotType.getText().toString() );			
			jsonObject.put("LOTNO", LotNo.getText().toString());
			jsonObject.put("FORMERLY_KNOWN_AS", Knownas.getText().toString() );
			jsonObject.put("BPM", Pekan.getText().toString());
			jsonObject.put("STATE", Daerah.getText().toString());
			jsonObject.put("AREA", Nageri.getText().toString());
			jsonObject.put("LOTAREA", LotArea.getText().toString());

			jsonObject.put("LASTUPDATEDON", LastUpdate.getText().toString());
			jsonObject.put("DEVELOPER", developerValue);
			jsonObject.put("DVLPR_CODE", developerValue_id);
			jsonObject.put("PROJECT_CODE", ProjectValue_id);
			jsonObject.put("PROJECTNAME",ProjectValue);
			jsonObject.put("DEVLICNO", DevLicense.getText().toString());
			jsonObject.put("DEVSOLICTOR", solicitorValue);
			jsonObject.put("DVLPR_SOL_CODE", solicitorValue_id);
			jsonObject.put("DVLPR_LOC", SolicitorLoc.getText().toString());
			jsonObject.put("LSTCHG_BANKCODE", bankValue_id);
			jsonObject.put("LSTCHG_BANKNAME", bankValue);
			jsonObject.put("LSTCHG_BRANCH", Branch .getText().toString());
			
			jsonObject.put("LSTCHG_PANO", PAName.getText().toString());
			jsonObject.put("LSTCHG_PRSTNO", PresentaionNo.getText().toString());

			
			
			//arrOfJson.put(jsonObject);
			System.out.println("JsonArray:"+jsonObject.toString());
			
		    //upDateList();

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
		
		RequestParams params = new RequestParams();
		params.put("sJsonInput", jsonObject.toString());
		System.out.println("params");
		System.out.println(params);

		RestService.post(METHOD_ADD_CASE2, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
				System.out.println("onFailure");

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("onFailure");
				System.out.println("AddCase2 Details Add Confirmed");
				System.out.println(arg2);

				// Find status Response
				try {
					StatusResult = jsonResponse.getString("Result").toString();
					messageDisplay = jsonResponse.getString("DisplayMessage").toString();
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				if (StatusResult.equals("SUCCESS")) {
					Intent iAddBack = new Intent(AddCaseStep2of4.this, AddCaseStep3of4.class);
					startActivity(iAddBack);

					Toast.makeText(AddCaseStep2of4.this, messageDisplay, Toast.LENGTH_SHORT).show();
				} else {
					Toast.makeText(AddCaseStep2of4.this, messageDisplay, Toast.LENGTH_SHORT).show();

				}
			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {

				// Get Json response
				arrayResponse = new JSONArray(arg0);
				jsonResponse = arrayResponse.getJSONObject(0);

				System.out.println("Addcase3 Details Add Response");
				System.out.println(arg0);
				return null;
			}
		});
		
		
		

	}
		
	
	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (resultCode == RESULT_OK) {
			System.out.println("workingthom2");
			if (requestCode == 1) {
				if (null == data)
					return;

				Uri selectedImageUri = data.getData();

				// MEDIA GALLERY
				System.out.println(selectedImageUri);
				selectedImagePath = selectedImageUri.getPath();
				//selectedImagePath = getRealPathFromURI(selectedImageUri);
				//ImageFilePath.getPath(getApplicationContext(), selectedImageUri);
				Log.i("Image File Path", "" + selectedImagePath);
				edittextFile.setText("File Path : " + selectedImagePath);
				messageText.setText("File Path : " + selectedImagePath);
			}
		}
	}

	
	public void addCaseListOfItems() {

		RequestParams params = null;
		params = new RequestParams();
		
		

		RestService.post(METHOD_ADDCASE_ITEMLIST, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Add Case List Item Success Details ");
				System.out.println(arg2);

				try {

					arrayResponse = new JSONArray(arg2);
					// Create new list
					jsonItemList = new ArrayList<HashMap<String, String>>();

					for (int i = 0; i < arrayResponse.length(); i++) {

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

				} catch (JSONException e) { // TODO Auto-generated
											// catch
											// block
					e.printStackTrace();
				}
				Toast.makeText(AddCaseStep2of4.this, "AddCase Item Found", Toast.LENGTH_SHORT).show();
				dialog.dismiss();
				// Simple Adapter for List
				SimpleAdapter simpleAdapter = new SimpleAdapter(AddCaseStep2of4.this, jsonItemList,
						R.layout.listview_column_addcase_itemlist, new String[] { "ItemCode_T", "ItemName_T" },
						new int[] { R.id.listAddCaseHeader_ItemCodeText, R.id.listAddCaseHeader_ItemNameText });

				listViewItem.setAdapter(simpleAdapter);
				
			

				listViewItem.setOnItemClickListener(new AdapterView.OnItemClickListener() {
					View ls=null;
					@Override
					public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
						
						
						if(get()!=view && get()!=null)
						{
							get().setActivated(false);
						}
						Toast.makeText(AddCaseStep2of4.this, "You Clicked at " + jsonItemList.get(position),
								Toast.LENGTH_SHORT).show();
						System.out.println(position);

						//int mSelectedItem = position;		
						 	
						 view.setActivated(true);
						 
						// Get list of Item data
						TextView c = (TextView) view.findViewById(R.id.listAddCaseHeader_ItemCodeText);
						itemCode = c.getText().toString();
						System.out.println(itemCode);
						String data = (String) parent.getItemAtPosition(position).toString();
						System.out.println(data);
						set(view);

					}

					private void set(View view) {						
						ls=view;						
					}
					private View get() {									
						return ls;
					}
				});

			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {

				// Get Json response
				arrayResponse = new JSONArray(arg0);
				jsonResponse = arrayResponse.getJSONObject(0);

				System.out.println("Add Case List Itemparse ParseResponse");
				System.out.println(arg0);
				return null;
			}
		});

	}

	public void dropdownTitle() throws JSONException {

		RequestParams params = null;
		params = new RequestParams();
		
		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "@AE_PROPERTY");
		jsonObject.put("FieldName", "TITLETYPE");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(TitleType_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
				System.out.println("Failed");

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("property Dropdown Success Details ");
				System.out.println(arg2);

				try {

					arrayResponse = new JSONArray(arg2);
					// Create new list
					jsonlistProject = new ArrayList<HashMap<String, String>>();

					for (int i = 0; i < arrayResponse.length(); i++) {

						jsonResponse = arrayResponse.getJSONObject(i);

						id = jsonResponse.getString("Id").toString();
						name = jsonResponse.getString("Name").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> proList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON  LIST");
						proList.put("Id_T", id);
						System.out.println(name);
						proList.put("Name_T", name);
						System.out.println(name);
						System.out.println(" END SEND JSON PROPERTY LIST");

						jsonlistProject.add(proList);
						System.out.println("JSON PROPERTY LIST");
						System.out.println(jsonlistProject);
					}
					// Spinner set Array Data in Drop down

					sAdapPROJ = new SimpleAdapter(AddCaseStep2of4.this, jsonlistProject, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyTitleType.setAdapter(sAdapPROJ);

					/*for (int j = 0; j < jsonlistProject.size(); j++) {
						if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
							TitleType_DROPDOWN.setSelection(j);
							break;
						}
					}*/

				} catch (JSONException e) { // TODO Auto-generated
											// catc
											// block
					e.printStackTrace();
				}

			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {

				// Get Json response
				arrayResponse = new JSONArray(arg0);
				jsonResponse = arrayResponse.getJSONObject(0);

				System.out.println("Property Dropdown Details parse Response");
				System.out.println(arg0);
				return null;
			}
		});

	}


	public void dropdownBankDeveloperSolicitor() {

		RequestParams params = null;
		params = new RequestParams();

		RestService.post(METHOD_PROPERTY_BDS_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Property Activity GetDropdown Success Details ");
				try {
					// Create new list
					jsonlistBank = new ArrayList<HashMap<String, String>>();
					jsonlistDeveloper = new ArrayList<HashMap<String, String>>();
					jsonlistSolicitor = new ArrayList<HashMap<String, String>>();

					jsonResponse = new JSONObject(arg2);

					JSONArray jsonBank = jsonResponse.getJSONArray("Bank");
					for (int j = 0; j < jsonBank.length(); j++) {
						JSONObject bank = jsonBank.getJSONObject(j);

						id_b = bank.getString("BankCode").toString();
						name_b = bank.getString("BankName").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> bankList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON BANK LIST");
						bankList.put("Id_T", id_b);
						System.out.println(name);
						bankList.put("Name_T", name_b);
						System.out.println(name);
						System.out.println(" END SEND JSON BANK LIST");

						jsonlistBank.add(bankList);
						System.out.println("JSON BANK LIST");
						System.out.println(jsonlistProject);

					}

					// Spinner set Array Data in Drop down

					sAdapBANK = new SimpleAdapter(AddCaseStep2of4.this, jsonlistBank, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyLSTCHG_BANKNAME.setAdapter(sAdapBANK);

					for (int j = 0; j < jsonlistBank.size(); j++) {
						if (jsonlistBank.get(j).get("Name_T").equals(bankDetailResponse)) {
							spinnerpropertyLSTCHG_BANKNAME.setSelection(j);
							break;
						}
					}

					JSONArray jsonDeveloper = jsonResponse.getJSONArray("Developer");
					for (int j = 0; j < jsonDeveloper.length(); j++) {
						JSONObject dev = jsonDeveloper.getJSONObject(j);
						id = dev.getString("DevCode").toString();
						name = dev.getString("DevName").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> devList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON DEV LIST");
						devList.put("Id_B", id);
						System.out.println(name);
						devList.put("Name_B", name);
						System.out.println(name);
						System.out.println(" END SEND JSON DEV LIST");

						jsonlistDeveloper.add(devList);
						System.out.println("JSON DEV LIST");
						System.out.println(jsonlistDeveloper);

					}
					// Spinner set Array Data in Drop down

					sAdapDEV = new SimpleAdapter(AddCaseStep2of4.this, jsonlistDeveloper, R.layout.spinner_item,
							new String[] { "Id_B", "Name_B" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyDEVELOPER.setAdapter(sAdapDEV);

					for (int j = 0; j < jsonlistDeveloper.size(); j++) {
						if (jsonlistDeveloper.get(j).get("Id_B").equals(developerCodeResponse)) {
							spinnerpropertyDEVELOPER.setSelection(j);
							break;
						}
					}

					JSONArray jsonSolicitor = jsonResponse.getJSONArray("Solicitor");
					for (int j = 0; j < jsonSolicitor.length(); j++) {
						JSONObject solic = jsonSolicitor.getJSONObject(j);
						id = solic.getString("SoliCode").toString();
						name = solic.getString("SoliName").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> solicList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON SOLICITOR LIST");
						solicList.put("Id_T", id);
						System.out.println(name);
						solicList.put("Name_T", name);
						System.out.println(name);
						System.out.println(" END SEND JSON SOLICITOR LIST");

						jsonlistSolicitor.add(solicList);
						System.out.println("JSON SOLICITOR LIST");
						System.out.println(jsonlistSolicitor);
					}

					// Spinner set Array Data in Drop down
					sAdapSOLIC = new SimpleAdapter(AddCaseStep2of4.this, jsonlistSolicitor, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyDEVSOLICTOR.setAdapter(sAdapSOLIC);

					for (int j = 0; j < jsonlistSolicitor.size(); j++) {
						if (jsonlistSolicitor.get(j).get("Id_T").equals(devSolictorCodeResponse)) {
							spinnerpropertyDEVSOLICTOR.setSelection(j);
							break;
						}
					}

				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}

				System.out.println(arg2);

			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {
				// Get Json response

				System.out.println("Property GetDropdown parse Response");
				System.out.println(arg0);
				return null;
			}

		});

	}

	public void dropdownPorject() {

		RequestParams params = null;
		params = new RequestParams();

		RestService.post(METHOD_PROPERTY_LIST_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("property Dropdown Success Details ");
				System.out.println(arg2);

				try {

					arrayResponse = new JSONArray(arg2);
					// Create new list
					jsonlistProject = new ArrayList<HashMap<String, String>>();

					for (int i = 0; i < arrayResponse.length(); i++) {

						jsonResponse = arrayResponse.getJSONObject(i);

						id = jsonResponse.getString("Id").toString();
						name = jsonResponse.getString("Name").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> proList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON  LIST");
						proList.put("Id_T", id);
						System.out.println(name);
						proList.put("Name_T", name);
						System.out.println(name);
						System.out.println(" END SEND JSON PROPERTY LIST");

						jsonlistProject.add(proList);
						System.out.println("JSON PROPERTY LIST");
						System.out.println(jsonlistProject);
					}
					// Spinner set Array Data in Drop down

					sAdapPROJ = new SimpleAdapter(AddCaseStep2of4.this, jsonlistProject, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyPROJECT.setAdapter(sAdapPROJ);

					for (int j = 0; j < jsonlistProject.size(); j++) {
						if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
							spinnerpropertyPROJECT.setSelection(j);
							break;
						}
					}

				} catch (JSONException e) { // TODO Auto-generated
											// catc
											// block
					e.printStackTrace();
				}

			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {

				// Get Json response
				arrayResponse = new JSONArray(arg0);
				jsonResponse = arrayResponse.getJSONObject(0);

				System.out.println("Property Dropdown Details parse Response");
				System.out.println(arg0);
				return null;
			}
		});

	}

	private PopupWindow pwindo;
	
	private void initiatePopupWindow() {
		try {
		// We need to get the instance of the LayoutInflater
		LayoutInflater inflater = (LayoutInflater) AddCaseStep2of4.this	.getSystemService(getApplicationContext().LAYOUT_INFLATER_SERVICE);
		View layout = inflater.inflate(R.layout.zoompdf,(ViewGroup) findViewById(R.id.popup_element),false);
		pwindo = new PopupWindow(layout, 600, 670, true);
		pwindo.showAtLocation(layout, Gravity.CENTER, 0, 0);
		

		btnClosePopup = (Button) layout.findViewById(R.id.btn_close_popup);
		btnClosePopup.setOnClickListener(cancel_button_click_listener);
		buttonconfirm  = (Button) layout.findViewById(R.id.button_PropertyConfirm);
		
		
		
			

		} catch (Exception e) {
		e.printStackTrace();
		}
		}
	
	
	private OnClickListener cancel_button_click_listener = new OnClickListener() {
		public void onClick(View v) {
		pwindo.dismiss();

		}
		};

		

		}

