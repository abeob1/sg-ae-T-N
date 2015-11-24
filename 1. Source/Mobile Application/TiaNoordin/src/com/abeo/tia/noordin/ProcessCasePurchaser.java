package com.abeo.tia.noordin;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Locale;

import android.app.DatePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.graphics.YuvImage;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.ZoomButton;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.Toast;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import abeo.tia.noordin.R;

/**
 * Created by Karthik on 10/7/2015.
 */
public class ProcessCasePurchaser extends BaseActivity implements
		OnClickListener {
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	//datepicker
	String myFormat = "MM/dd/yyyy";
	Calendar myCalendar = Calendar.getInstance();
	
	Button vendor_btn, details_btn, property_btn, loan_principal_btn,
			loan_subsidary_btn, process_btn, confirm_btn,purchaser_btn,walkin;
	ZoomButton zoom1,zoom2,zoom3,zoom4;

	EditText case_type, file_open_date, case_file_no;
	EditText spa_date, entry_private_caveat, withdrawal_private_caveat, name1,
			brn_no1, tax_no1, contact_no1, type_1, name2, brn_no2, tax_no2,
			contact_no2, type_2, name3, brn_no3, tax_no3, contact_no3, type_3,
			name4, brn_no4, tax_no4, contact_no4, type_4;
	private final String METHOD_PROCESS_CASE_DETAILS = "SPA_ProcessCase_GetDataFromOCRD";

	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null,jsonResponseconfirm = null;

	// spinner declaration
	Spinner spinner_case_status, spinner_kiv, spinner_type1, spinner_type2,
			spinner_type3, spinner_type4;
	TextView ID, TEXT;
	String caseValue_id = "", titleValue = "", casetype = "",
			casetype_value = "";
	

	
			
	// checkbox
	CheckBox rep_firm, lawer_rep;

	// Get Project value fromapi
	private final String GET_SPINNER_VALUES = "SPA_GetValidValues";
	ArrayList<HashMap<String, String>> jsonArraylist = null;
	String id, name;
	SimpleAdapter sAdapPROJ;
	String GET_TYPE_SPINNER = "SPA_ProcessCase_GetIDType";
	
	//Confirm btn request URL
	String CONFIRM_BTN_REQUEST = "SPA_ProcessCase_UpdateCaseTabDetails";
	
	String messageDisplay = "", StatusResult = "";
	String f_yes, l_yes,Scase_status, Skiv, Stype1, Stype2,	Stype3, Stype4;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_process_case_purchaser);

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

		// Find the SharedPreferences pass Login value
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData",
				Context.MODE_PRIVATE);
		System.out.println("LOGIN DATA");
		String userName = prefLoginReturn.getString("sUserName", "");

		String category = prefLoginReturn.getString("sCategory", "");
		System.out.println(category);
		String CardCode = prefLoginReturn.getString("CardCode", "");
		System.out.println(CardCode);
		
		// Find All zoom button
					zoom1 = (ZoomButton) findViewById(R.id.zoomButton_PurchaserPdf1);
					zoom2 = (ZoomButton) findViewById(R.id.zoomButton_PurchaserPdf2);
					zoom3 = (ZoomButton) findViewById(R.id.zoomButton_PurchaserPdf3);
					zoom4 = (ZoomButton) findViewById(R.id.zoomButton_PurchaserPdf4);
					
					
					

		// Button Init
		vendor_btn = (Button) findViewById(R.id.button_PurchaserVendor);
		details_btn = (Button) findViewById(R.id.button_PurchaserDetails);
		property_btn = (Button) findViewById(R.id.button_PurchaserProperty);
		loan_principal_btn = (Button) findViewById(R.id.button_PurchaserLoanPrincipal);
		loan_subsidary_btn = (Button) findViewById(R.id.button_PurchaserLoanSubsidiary);
		process_btn = (Button) findViewById(R.id.button_PurchaserProcess);
		confirm_btn = (Button) findViewById(R.id.button_PurchaserConfirm);
		purchaser_btn = (Button) findViewById(R.id.button_ProcessCase1Purchaser);

		// Edittext Init
		case_type = (EditText) findViewById(R.id.editText_PurchaserCaseType);
		case_file_no = (EditText) findViewById(R.id.editText_PurchaserCaseFileNo);
		file_open_date = (EditText) findViewById(R.id.editText_PurchaserFileOpenDate);
		spa_date = (EditText) findViewById(R.id.editText_PurchaserSPADate);
		entry_private_caveat = (EditText) findViewById(R.id.editText_PurchaserEnteryOfPrivateCaveat);
		withdrawal_private_caveat = (EditText) findViewById(R.id.editText_PurchaserWithdrawalOfPrivateCaveat);
		name1 = (EditText) findViewById(R.id.editText_PurchaserName1st);
		brn_no1 = (EditText) findViewById(R.id.editText_PurchaserIdBrnNo);
		tax_no1 = (EditText) findViewById(R.id.editText_PurchaserTaxNo);
		contact_no1 = (EditText) findViewById(R.id.editText_PurchaserContactNo);
		name2 = (EditText) findViewById(R.id.editText_PurchaserName2nd);
		brn_no2 = (EditText) findViewById(R.id.editText_PropertyDaerahState);
		tax_no2 = (EditText) findViewById(R.id.editText_PropertyNageriArea);
		contact_no2 = (EditText) findViewById(R.id.editText_PurchaserContactNo2);
		name3 = (EditText) findViewById(R.id.editText_PurchaserName3rd);
		brn_no3 = (EditText) findViewById(R.id.editText_PurchaserIdBrnNo3);
		tax_no3 = (EditText) findViewById(R.id.editText_PurchaserTaxNo3);
		contact_no3 = (EditText) findViewById(R.id.editText_PurchaserContactNo3);
		name4 = (EditText) findViewById(R.id.editText_PurchaserName4th);
		brn_no4 = (EditText) findViewById(R.id.editText_PropertyDaerahState4);
		tax_no4 = (EditText) findViewById(R.id.editText_PropertyNageriArea4);
		contact_no4 = (EditText) findViewById(R.id.editText_PurchaserContactNo4);
		walkin = (Button) findViewById(R.id.button_PurchaserWalkin);

		// spinners initialization
		spinner_case_status = (Spinner) findViewById(R.id.case_status);
		spinner_kiv = (Spinner) findViewById(R.id.spinner_ProcessCase1KIV);
		spinner_type1 = (Spinner) findViewById(R.id.spinner_PurchaserType);
		spinner_type2 = (Spinner) findViewById(R.id.spinner_PurchaserType2);
		spinner_type3 = (Spinner) findViewById(R.id.spinner_PurchaserType3);
		spinner_type4 = (Spinner) findViewById(R.id.spinner_PurchaserType4);

		// checkbox
		rep_firm = (CheckBox) findViewById(R.id.chk_rep_by_firm);
		lawer_rep = (CheckBox) findViewById(R.id.chk_lawer);
		

		vendor_btn.setOnClickListener(this);
		details_btn.setOnClickListener(this);
		property_btn.setOnClickListener(this);
		loan_subsidary_btn.setOnClickListener(this);
		loan_principal_btn.setOnClickListener(this);
		process_btn.setOnClickListener(this);
		confirm_btn.setOnClickListener(this);
		purchaser_btn.setOnClickListener(this);
		walkin.setOnClickListener(this);
		
		zoom1.setOnClickListener(this);
		zoom2.setOnClickListener(this);
		zoom3.setOnClickListener(this);
		zoom4.setOnClickListener(this);
		
		//datepicker
		withdrawal_private_caveat.setOnClickListener(this);
		entry_private_caveat.setOnClickListener(this);
		spa_date.setOnClickListener(this);

		try {
			setvaluestoUI();
		} catch (JSONException e) {
			e.printStackTrace();
		}

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

		// Spinner click listener
		spinner_kiv
				.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent,
							View view, int position, long id) {
						ID = (TextView) view.findViewById(R.id.Id);
						casetype = ID.getText().toString();
						TEXT = (TextView) view.findViewById(R.id.Name);
						casetype_value = TEXT.getText().toString();
					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
		// Spinner click listener
		spinner_type1
				.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent,
							View view, int position, long id) {

					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
		// Spinner click listener
		spinner_type2
				.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent,
							View view, int position, long id) {

					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
		// Spinner click listener
		spinner_type3
				.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent,
							View view, int position, long id) {

					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
		// Spinner click listener
		spinner_type4
				.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent,
							View view, int position, long id) {
					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});

		
		disableHeaderfields();
	}
	
	private void disableHeaderfields() {
		// TODO Auto-generated method stub
	file_open_date.setEnabled(false);
    case_type.setEnabled(false);
    case_file_no.setEnabled(false); 
    spinner_case_status.setEnabled(false);
    spinner_kiv.setEnabled(false);
		
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
							jsonArraylist = new ArrayList<HashMap<String, String>>();

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

								jsonArraylist.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylist);
							}
							// Spinner set Array Data in Drop down

							sAdapPROJ = new SimpleAdapter(
									ProcessCasePurchaser.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_kiv.setAdapter(sAdapPROJ);

							
							  for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(
									  Skiv)) {
								  spinner_kiv.setSelection(j); break; } }
							 

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}

	public void dropdowntype1() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		/*
		 * jsonObject.put("TableName", "OCRD"); jsonObject.put("FieldName",
		 * "KIVSTATUS");
		 */
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(GET_TYPE_SPINNER, params,
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
							jsonArraylist = new ArrayList<HashMap<String, String>>();

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

								jsonArraylist.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylist);
							}
							// Spinner set Array Data in Drop down

							sAdapPROJ = new SimpleAdapter(
									ProcessCasePurchaser.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							// All adapters has been set here
							spinner_type1.setAdapter(sAdapPROJ);
							spinner_type2.setAdapter(sAdapPROJ);
							spinner_type3.setAdapter(sAdapPROJ);
							spinner_type4.setAdapter(sAdapPROJ);
							
							System.out.println("Thomas Test Type");
							System.out.println(Stype1);
							  for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(
									  Stype1)) {
								  spinner_type1.setSelection(j); break; } }
							
							for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(
									  Stype2)) {
								  spinner_type2.setSelection(j); break; } }
							
							for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(
									  Stype3)) {
								  spinner_type1.setSelection(j); break; } }
							
							for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(
									  Stype3)) {
								  spinner_type1.setSelection(j); break; } }
							
							for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(
									  Stype4)) {
								  spinner_type4.setSelection(j); break; } }
							 

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
							jsonArraylist = new ArrayList<HashMap<String, String>>();

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

								jsonArraylist.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylist);
							}
							// Spinner set Array Data in Drop down

							sAdapPROJ = new SimpleAdapter(
									ProcessCasePurchaser.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_case_status.setAdapter(sAdapPROJ);

							
							  for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(Scase_status)) {
								  spinner_case_status.setSelection(j); break; } }
							 

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}

	private void setvaluestoUI() throws JSONException {
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

					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out
								.println("property Dropdown Success Details ");
						System.out.println(arg2);
						try {
							JSONArray arrayRes = new JSONArray(arg2);
							jsonResponseconfirm =arrayRes.getJSONObject(0);
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
						setallvalues(arg2);
						

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

		JSONArray jObj = null;
		try {
			jObj = new JSONArray(arg2.toString());

			for (int i = 0; i < jObj.length(); i++) {
				JSONObject jsonobject = jObj.getJSONObject(i);
				file_open_date.setText(jsonobject.getString("FileOpenDate"));
				case_file_no.setText(jsonobject.getString("CaseFileNo"));
				case_type.setText(jsonobject.getString("CaseType"));
				Scase_status = jsonobject.getString("CaseStatus");
				

				JSONObject obj1 = jsonobject.getJSONObject("Purchaser");

				spa_date.setText(obj1.getString("PurSPADate"));
				entry_private_caveat.setText(obj1
						.getString("PurEntryOfPrivateCaveat"));
				withdrawal_private_caveat.setText(obj1
						.getString("PurWithOfPrivateCaveat"));
				name1.setText(obj1.getString("PurFirstName"));
				brn_no1.setText(obj1.getString("PurFirstID"));
				tax_no1.setText(obj1.getString("PurFirstTaxNo"));
				contact_no1.setText(obj1.getString("PurFirstContactNo"));
				name2.setText(obj1.getString("PurSecName"));
				brn_no2.setText(obj1.getString("PurSecID"));
				tax_no2.setText(obj1.getString("PurSecTaxNo"));
				contact_no2.setText(obj1.getString("PurSecContactNo"));
				name3.setText(obj1.getString("PurThirdName"));
				brn_no3.setText(obj1.getString("PurThirdID"));
				tax_no3.setText(obj1.getString("PurThirdTaxNo"));
				contact_no3.setText(obj1.getString("PurThirdContactNo"));
				name4.setText(obj1.getString("PurFourthName"));
				brn_no4.setText(obj1.getString("PurFourthID"));
				tax_no4.setText(obj1.optString("PurFourthTaxNo"));
				contact_no4.setText(obj1.getString("PurFourthContactNo"));
				
				if(!jsonobject.getString("KIV").equals(""))
					Skiv = jsonobject.getString("KIV");
				if(!obj1.getString("PurFirstType").equals(""))
					Stype1 = obj1.getString("PurFirstType");
				if(!obj1.getString("PurSecType").equals(""))
					Stype2 = obj1.getString("PurSecType");
				if(!obj1.getString("PurThirdType").equals(""))
					Stype3 = obj1.getString("PurThirdType");
				if(!obj1.getString("PurFourthType").equals(""))
					Stype4 = obj1.getString("PurFourthType");
				 

				if (obj1.getString("PurRepresentedByFirm").equals("N")) {
					rep_firm.setChecked(false);
					//rep_firm.setEnabled(false);
				} else {
					rep_firm.setChecked(true);
					//rep_firm.setEnabled(false);
				}
				if (obj1.getString("PurlawyerRepresented").equals("N")) {
					lawer_rep.setChecked(false);
					//lawer_rep.setEnabled(false);
				} else {
					lawer_rep.setChecked(true);
					//lawer_rep.setEnabled(false);
				}
			}
			System.out.println(jObj);
		} catch (JSONException e) {
			e.printStackTrace();
		}
		
		try {
			dropdownstatus();		
			dropdownKIV();
			dropdowntype1();
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	@Override
	public void onClick(View view) {
		if (view== details_btn) {
			 Intent to_purchaser = new Intent(ProcessCasePurchaser.this, ProcessCaseDetails.class);
	           startActivity(to_purchaser);
		}
		if (view == purchaser_btn) {
	        Intent to_purchaser = new Intent(ProcessCasePurchaser.this, ProcessCasePurchaser.class);
	        startActivity(to_purchaser);
	    }
	    if (view == vendor_btn) {
	        Intent to_vendor = new Intent(ProcessCasePurchaser.this, ProcessCaseVendor.class);
	        startActivity(to_vendor);
	    }
	    if (view == property_btn) {
	        Intent to_loan_pricipal = new Intent(ProcessCasePurchaser.this, ProcessCaseProperty.class);
	        startActivity(to_loan_pricipal);
	    }
	    if (view == loan_principal_btn) {
	        Intent to_loan_pricipal = new Intent(ProcessCasePurchaser.this, ProcessCaseLoanPrincipal.class);
	        startActivity(to_loan_pricipal);
	    }
	    if (view == loan_subsidary_btn) {
	        Intent to_loan_subsidiary = new Intent(ProcessCasePurchaser.this, ProcesscaseLoanSubsidiary.class);
	        startActivity(to_loan_subsidiary);
	    }
	    if (view == process_btn) {
	        Intent to_loan_subsidiary = new Intent(ProcessCasePurchaser.this, ProcessCaseProcessTab.class);
	        startActivity(to_loan_subsidiary);
	    }
	    if(view == walkin)
        {
        	Intent i = new Intent(ProcessCasePurchaser.this, WalkInActivity.class);
			startActivity(i);
        }
		if (view == confirm_btn) {
			try {
				confirm_allvalues_btn();
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		if(view == zoom1)
        {
			String selected = getSelectedItem(((Spinner)findViewById(R.id.spinner_PurchaserType)).getSelectedItem().toString());
			
			try {
				
			if(selected.equals("CORPORATE"))				
				zoombtncor(name1.getText().toString() , contact_no1.getText().toString() , brn_no1.getText().toString() , catg);				
			if(selected.equals("INDIVIDUAL"))
				zoombtnin(name1.getText().toString() , contact_no1.getText().toString() , brn_no1.getText().toString() , catg);
			
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
        }
		if(view == zoom2)
        {
			String selected = getSelectedItem(((Spinner)findViewById(R.id.spinner_PurchaserType2)).getSelectedItem().toString());
			
			try {
				
			if(selected.equals("CORPORATE"))				
				zoombtncor(name2.getText().toString() , contact_no2.getText().toString() , brn_no2.getText().toString() , catg);				
			if(selected.equals("INDIVIDUAL"))
				zoombtnin(name2.getText().toString() , contact_no2.getText().toString() , brn_no2.getText().toString() , catg);
			
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
        }
		if(view == zoom3)
        {
			String selected = getSelectedItem(((Spinner)findViewById(R.id.spinner_PurchaserType3)).getSelectedItem().toString());
		
			try {
				
			if(selected.equals("CORPORATE"))				
				zoombtncor(name3.getText().toString() , contact_no3.getText().toString() , brn_no3.getText().toString() , catg);				
			if(selected.equals("INDIVIDUAL"))
				zoombtnin(name3.getText().toString() , contact_no3.getText().toString() , brn_no3.getText().toString() , catg);
			
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
        }
		if(view == zoom4)
        {
			String selected = getSelectedItem(((Spinner)findViewById(R.id.spinner_PurchaserType4)).getSelectedItem().toString());
			
			try {
				
			if(selected.equals("CORPORATE"))				
				zoombtncor(name4.getText().toString() , contact_no4.getText().toString() , brn_no4.getText().toString() , catg);				
			if(selected.equals("INDIVIDUAL"))
				zoombtnin(name4.getText().toString() , contact_no4.getText().toString() , brn_no4.getText().toString() , catg);
			
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
        }
		
		
		
		if(view == withdrawal_private_caveat)
        {
			 new DatePickerDialog(ProcessCasePurchaser.this, datew, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }		
		if(view == entry_private_caveat)
        {
			 new DatePickerDialog(ProcessCasePurchaser.this, dateentry, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }		
		if(view == spa_date)
        {
			 new DatePickerDialog(ProcessCasePurchaser.this, datespa, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }
	}
	
	DatePickerDialog.OnDateSetListener datew = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        withdrawal_private_caveat.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener dateentry = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        entry_private_caveat.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener datespa = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        spa_date.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	public String getSelectedItem(String a)
	{
		String[] separated = a.split(",");
		String[] finaltext = separated[0].split("="); 
		return finaltext[1];
		
	}

	private void confirm_allvalues_btn() throws JSONException {
		// TODO Auto-generated method stub
		
		String spinnertype1 = getSelectedItem(((Spinner)findViewById(R.id.spinner_PurchaserType)).getSelectedItem().toString());
		String spinnertype2 = getSelectedItem(((Spinner)findViewById(R.id.spinner_PurchaserType2)).getSelectedItem().toString());
		String spinnertype3 = getSelectedItem(((Spinner)findViewById(R.id.spinner_PurchaserType3)).getSelectedItem().toString());
		String spinnertype4 = getSelectedItem(((Spinner)findViewById(R.id.spinner_PurchaserType4)).getSelectedItem().toString());
		
		if(rep_firm.isChecked()){
			f_yes= "Y";
		}else if(!rep_firm.isChecked()){
			f_yes = "N";
		}
		if (lawer_rep.isChecked()) {
			l_yes = "Y";
		}else if (!lawer_rep.isChecked()) {
			l_yes = "N";
		}
				
	//	String json_element = "[{\"Case\":\"1500000001\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"CaseStatus\":\"OPEN\",\"FileOpenDate\":\"10/10/2015 12:00:00 AM\",\"CaseFileNo\":\"JJ/1500000001/\",\"KIV\":\"\",\"TabId\":\"2\",\"Details\":{\"LA\":\"\",\"MANAGER\":\"\",\"InCharge\":\"\",\"CustomerService\":\"\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"FileLocation\":\"\",\"FileClosedDate\":\"\",\"VendAcqDt\":\"\",\"CompanyBuisnessSearch\":\"\",\"BankWindingSearch\":\"\"},\"Purchaser\":{\"PurRepresentedByFirm\":\"N\",\"PurlawyerRepresented\":\"N\",\"PurSPADate\":\"1/1/1900 12:00:00 AM\",\"PurEntryOfPrivateCaveat\":\"Chase\",\"PurWithOfPrivateCaveat\":\"Rane\",\"PurFirstName\":\"Name1\",\"PurFirstID\":\"Id1\",\"PurFirstTaxNo\":\"Tax1\",\"PurFirstContactNo\":\"9784561233\",\"PurFirstType\":\"CORPORATE\",\"PurSecName\":\"Name2\",\"PurSecID\":\"Id2\",\"PurSecTaxNo\":\"Tax2\",\"PurSecContactNo\":\"9784561234\",\"PurSecType\":\"INDIVIDUAL\",\"PurThirdName\":\"Name3\",\"PurThirdID\":\"Id3\",\"PurThirdTaxNo\":\"Tax3\",\"PurThirdContactNo\":\"9784561234\",\"PurThirdType\":\"INDIVIDUAL\",\"PurFourthName\":\"Name4\",\"PurFourthID\":\"Id4\",\"PurFourthTaxNo\":\"Tax4\",\"PurFourthContactNo\":\"9784561235\",\"PurFourthType\":\"INDIVIDUAL\"},\"Vendor\":{\"VndrRepresentedByFirm\":\"N\",\"VndrlawyerRepresented\":\"Y\",\"VndrReqDevConsent\":\"Name\",\"VndrReceiveDevConsent\":\"Sam\",\"VndrFirstName\":\"Name1\",\"VndrFirstID\":\"Id1\",\"VndrFirstTaxNo\":\"Tax1\",\"VndrFirstContactNo\":\"9784561233\",\"VndrFirstType\":\"CORPORATE\",\"VndrSecName\":\"Name2\",\"VndrSecID\":\"Id2\",\"VndrSecTaxNo\":\"Tax2\",\"VndrSecContactNo\":\"9784561234\",\"VndrSecType\":\"INDIVIDUAL\",\"VndrThirdName\":\"Name3\",\"VndrThirdID\":\"Id3\",\"VndrThirdTaxNo\":\"Tax3\",\"VndrThirdContactNo\":\"9784561234\",\"VndrThirdType\":\"INDIVIDUAL\",\"VndrFourthName\":\"Name4\",\"VndrFourthID\":\"Id4\",\"VndrFourthTaxNo\":\"Tax4\",\"VndrFourthContactNo\":\"9784561235\",\"VndrFourthType\":\"INDIVIDUAL\"},\"Property\":{\"TitleType\":\"KEKAL\",\"CertifiedPlanNo\":\"\",\"LotNo\":\"\",\"PreviouslyKnownAs\":\"\",\"State\":\"\",\"Area\":\"\",\"BPM\":\"\",\"GovSurvyPlan\":\"\",\"LotArea\":\"12345\",\"Developer\":\"\",\"Project\":\"\",\"DevLicenseNo\":\"\",\"DevSolicitor\":\"\",\"DevSoliLoc\":\"\",\"TitleSearchDate\":\"\",\"DSCTransfer\":\"\",\"DRCTransfer\":\"\",\"FourteenADate\":\"\",\"DRTLRegistry\":\"\",\"PropertyCharged\":\"\",\"BankName\":\"\",\"Branch\":\"\",\"PAName\":\"\",\"PresentationNo\":\"\",\"ExistChargeRef\":\"\",\"ReceiptType\":\"\",\"ReceiptNo\":\"\",\"ReceiptDate\":\"\",\"PurchasePrice\":\"1452\",\"AdjValue\":\"\",\"VndrPrevSPAValue\":\"\",\"Deposit\":\"\",\"BalPurPrice\":\"\",\"LoanAmount\":\"\",\"LoanCaseNo\":\"1234\",\"DiffSum\":\"\",\"RedAmt\":\"\",\"RedDate\":\"\",\"DefRdmptSum\":\"\"},\"LoanPrinciple\":{\"ReqRedStatement\":\"\",\"RedStmtDate\":\"\",\"RedPayDate\":\"\",\"RepByFirm\":\"\",\"LoanCaseNo\":\"1234\",\"Project\":\"\",\"MasterBankName\":\"\",\"BranchName\":\"\",\"Address\":\"\",\"PAName\":\"\",\"BankRef\":\"\",\"BankInsDate\":\"2015/10/11\",\"LOFDate\":\"\",\"BankSolicitor\":\"\",\"SoliLoc\":\"\",\"SoliRef\":\"\",\"TypeofLoan\":\"\",\"TypeofFacility\":\"\",\"FacilityAmt\":\"\",\"Repaymt\":\"\",\"IntrstRate\":\"\",\"MonthlyInstmt\":\"\",\"TermLoanAmt\":\"\",\"Interest\":\"\",\"ODLoan\":\"\",\"MRTA\":\"\",\"BankGuarantee\":\"\",\"LetterofCredit\":\"\",\"TrustReceipt\":\"\",\"Others\":\"\",\"LoanDet1\":\"\",\"LoanDet2\":\"\",\"LoanDet3\":\"\",\"LoanDet4\":\"\",\"LoanDet5\":\"\"},\"LoanSubsidary\":{\"LoanDocForBankExe\":\"Exe 1\",\"FaciAgreeDate\":\"\",\"LoanDocRetFromBank\":\"\",\"DischargeofCharge\":\"\",\"FirstTypeofFacility\":\"\",\"FirstFacilityAmt\":\"\",\"FirstRepaymt\":\"\",\"FirstIntrstRate\":\"\",\"FirstMonthlyInstmt\":\"1500\",\"FirstTermLoanAmt\":\"\",\"FirstInterest\":\"\",\"FirstODLoan\":\"\",\"FirstMRTA\":\"\",\"FirstBankGuarantee\":\"\",\"FirstLetterofCredit\":\"\",\"FirstTrustReceipt\":\"\",\"FirstOthers\":\"Sample\",\"SecTypeofFacility\":\"\",\"SecFacilityAmt\":\"\",\"SecRepaymt\":\"\",\"SecIntrstRate\":\"12%\",\"SecMonthlyInstmt\":\"\",\"SecTermLoanAmt\":\"\",\"SecInterest\":\"\",\"SecODLoan\":\"\",\"SecMRTA\":\"\",\"SecBankGuarantee\":\"\",\"SecLetterofCredit\":\"\",\"SecTrustReceipt\":\"45A\",\"SecOthers\":\"\",\"ThirdTypeofFacility\":\"Sample_Data\",\"ThirdFacilityAmt\":\"\",\"ThirdRepaymt\":\"\",\"ThirdIntrstRate\":\"\",\"ThirdMonthlyInstmt\":\"\",\"ThirdTermLoanAmt\":\"587.15\",\"ThirdInterest\":\"\",\"ThirdODLoan\":\"\",\"ThirdMRTA\":\"\",\"ThirdBankGuarantee\":\"\",\"ThirdLetterofCredit\":\"\",\"ThirdTrustReceipt\":\"\",\"ThirdOthers\":\"\",\"FourthTypeofFacility\":\"Sample4\",\"FourthFacilityAmt\":\"\",\"FourthRepaymt\":\"15\",\"FourthIntrstRate\":\"\",\"FourthMonthlyInstmt\":\"\",\"FourthTermLoanAmt\":\"\",\"FourthInterest\":\"21\",\"FourthODLoan\":\"\",\"FourthMRTA\":\"\",\"FourthBankGuarantee\":\"\",\"FourthLetterofCredit\":\"\",\"FourthTrustReceipt\":\"\",\"FourthOthers\":\"\",\"FifthTypeofFacility\":\"Sample 5\",\"FifthFacilityAmt\":\"\",\"FifthRepaymt\":\"\",\"FifthIntrstRate\":\"\",\"FifthMonthlyInstmt\":\"\",\"FifthTermLoanAmt\":\"\",\"FifthInterest\":\"10\",\"FifthODLoan\":\"\",\"FifthMRTA\":\"\",\"FifthBankGuarantee\":\"\",\"FifthLetterofCredit\":\"\",\"FifthTrustReceipt\":\"Test\",\"FifthOthers\":\"\"}}]";
		String json_element = "[{\"Case\":" + "\"" + jsonResponseconfirm.get("Case").toString() + "\",\"CaseType\":" + "\"" + jsonResponseconfirm.get("CaseType").toString() + "\",\"CaseStatus\":\"OPEN\",\"FileOpenDate\":\"10/10/2015 12:00:00 AM\",\"CaseFileNo\":\"JJ/1500000001/\",\"KIV\":\"\",\"TabId\":\"2\",\"Details\":{\"LA\":\"\",\"MANAGER\":\"\",\"InCharge\":\"\",\"CustomerService\":\"\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"FileLocation\":\"\",\"FileClosedDate\":\"\",\"VendAcqDt\":\"\",\"CompanyBuisnessSearch\":\"\",\"BankWindingSearch\":\"\"},\"Purchaser\":{\"" + "PurRepresentedByFirm" + "\":" + "\"" + f_yes + "\",\"" + "PurlawyerRepresented" + "\":" + "\"" + l_yes + "\",\"" + "PurSPADate" + "\":" + "\"" + spa_date.getText().toString() + "\", \"" + "PurEntryOfPrivateCaveat" + "\":" + "\"" + entry_private_caveat.getText().toString() + "\", \"" + "PurWithOfPrivateCaveat" + "\":" + "\"" + withdrawal_private_caveat.getText().toString() + "\" ,\"" + "PurFirstName" + "\":" + "\"" + name1.getText().toString() + "\",\"" + "PurFirstID" + "\":" + "\"" + brn_no1.getText().toString() + "\",\"" + "PurFirstTaxNo" + "\":" + "\"" + tax_no1.getText().toString() + "\",\"" + "PurFirstContactNo" + "\":" + "\"" + contact_no1.getText().toString() + "\",\"" + "PurFirstType" + "\":" + "\"" + spinnertype1 + "\",\"" + "PurSecName" + "\":" + "\"" + name2.getText().toString() + "\",\"" + "PurSecID" + "\":" + "\"" + brn_no2.getText().toString() + "\",\"" + "PurSecTaxNo" + "\":" + "\"" + tax_no2.getText().toString() + "\",\"" + "PurSecContactNo" + "\":" + "\"" + contact_no2.getText().toString() + "\",\"" + "PurSecType" + "\":" + "\"" + spinnertype2 + "\",\"" + "PurThirdName" + "\":" + "\"" + name3.getText().toString() + "\",\"" + "PurThirdID" + "\":" + "\"" + brn_no3.getText().toString() + "\",\"" + "PurThirdTaxNo" + "\":" + "\"" + tax_no3.getText().toString() + "\",\"" + "PurThirdContactNo" + "\":" + "\"" + contact_no3.getText().toString() + "\",\"" + "PurThirdType" + "\":" + "\"" + spinnertype3 + "\",\"" + "PurFourthName" + "\":" + "\"" + name4.getText().toString() + "\",\"" + "PurFourthID" + "\":" + "\"" + brn_no4.getText().toString() + "\",\"" + "PurFourthTaxNo" + "\":" + "\"" + tax_no4.getText().toString() + "\",\"" + "PurFourthContactNo" + "\":" + "\"" + contact_no4.getText().toString() + "\",\"" + "PurFourthType" + "\":" + "\"" + spinnertype4 + "\"},\"Vendor\":{\"VndrRepresentedByFirm\":\"N\",\"VndrlawyerRepresented\":\"Y\",\"VndrReqDevConsent\":\"Name\",\"VndrReceiveDevConsent\":\"Sam\",\"VndrFirstName\":\"Name1\",\"VndrFirstID\":\"Id1\",\"VndrFirstTaxNo\":\"Tax1\",\"VndrFirstContactNo\":\"9784561233\",\"VndrFirstType\":\"CORPORATE\",\"VndrSecName\":\"Name2\",\"VndrSecID\":\"Id2\",\"VndrSecTaxNo\":\"Tax2\",\"VndrSecContactNo\":\"9784561234\",\"VndrSecType\":\"INDIVIDUAL\",\"VndrThirdName\":\"Name3\",\"VndrThirdID\":\"Id3\",\"VndrThirdTaxNo\":\"Tax3\",\"VndrThirdContactNo\":\"9784561234\",\"VndrThirdType\":\"INDIVIDUAL\",\"VndrFourthName\":\"Name4\",\"VndrFourthID\":\"Id4\",\"VndrFourthTaxNo\":\"Tax4\",\"VndrFourthContactNo\":\"9784561235\",\"VndrFourthType\":\"INDIVIDUAL\"},\"Property\":{\"TitleType\":\"KEKAL\",\"CertifiedPlanNo\":\"\",\"LotNo\":\"\",\"PreviouslyKnownAs\":\"\",\"State\":\"\",\"Area\":\"\",\"BPM\":\"\",\"GovSurvyPlan\":\"\",\"LotArea\":\"12345\",\"Developer\":\"\",\"Project\":\"\",\"DevLicenseNo\":\"\",\"DevSolicitor\":\"\",\"DevSoliLoc\":\"\",\"TitleSearchDate\":\"\",\"DSCTransfer\":\"\",\"DRCTransfer\":\"\",\"FourteenADate\":\"\",\"DRTLRegistry\":\"\",\"PropertyCharged\":\"\",\"BankName\":\"\",\"Branch\":\"\",\"PAName\":\"\",\"PresentationNo\":\"\",\"ExistChargeRef\":\"\",\"ReceiptType\":\"\",\"ReceiptNo\":\"\",\"ReceiptDate\":\"\",\"PurchasePrice\":\"1452\",\"AdjValue\":\"\",\"VndrPrevSPAValue\":\"\",\"Deposit\":\"\",\"BalPurPrice\":\"\",\"LoanAmount\":\"\",\"LoanCaseNo\":\"1234\",\"DiffSum\":\"\",\"RedAmt\":\"\",\"RedDate\":\"\",\"DefRdmptSum\":\"\"},\"LoanPrinciple\":{\"ReqRedStatement\":\"\",\"RedStmtDate\":\"\",\"RedPayDate\":\"\",\"RepByFirm\":\"\",\"LoanCaseNo\":\"1234\",\"Project\":\"\",\"MasterBankName\":\"\",\"BranchName\":\"\",\"Address\":\"\",\"PAName\":\"\",\"BankRef\":\"\",\"BankInsDate\":\"2015/10/11\",\"LOFDate\":\"\",\"BankSolicitor\":\"\",\"SoliLoc\":\"\",\"SoliRef\":\"\",\"TypeofLoan\":\"\",\"TypeofFacility\":\"\",\"FacilityAmt\":\"\",\"Repaymt\":\"\",\"IntrstRate\":\"\",\"MonthlyInstmt\":\"\",\"TermLoanAmt\":\"\",\"Interest\":\"\",\"ODLoan\":\"\",\"MRTA\":\"\",\"BankGuarantee\":\"\",\"LetterofCredit\":\"\",\"TrustReceipt\":\"\",\"Others\":\"\",\"LoanDet1\":\"\",\"LoanDet2\":\"\",\"LoanDet3\":\"\",\"LoanDet4\":\"\",\"LoanDet5\":\"\"},\"LoanSubsidary\":{\"LoanDocForBankExe\":\"Exe 1\",\"FaciAgreeDate\":\"\",\"LoanDocRetFromBank\":\"\",\"DischargeofCharge\":\"\",\"FirstTypeofFacility\":\"\",\"FirstFacilityAmt\":\"\",\"FirstRepaymt\":\"\",\"FirstIntrstRate\":\"\",\"FirstMonthlyInstmt\":\"1500\",\"FirstTermLoanAmt\":\"\",\"FirstInterest\":\"\",\"FirstODLoan\":\"\",\"FirstMRTA\":\"\",\"FirstBankGuarantee\":\"\",\"FirstLetterofCredit\":\"\",\"FirstTrustReceipt\":\"\",\"FirstOthers\":\"Sample\",\"SecTypeofFacility\":\"\",\"SecFacilityAmt\":\"\",\"SecRepaymt\":\"\",\"SecIntrstRate\":\"12%\",\"SecMonthlyInstmt\":\"\",\"SecTermLoanAmt\":\"\",\"SecInterest\":\"\",\"SecODLoan\":\"\",\"SecMRTA\":\"\",\"SecBankGuarantee\":\"\",\"SecLetterofCredit\":\"\",\"SecTrustReceipt\":\"45A\",\"SecOthers\":\"\",\"ThirdTypeofFacility\":\"Sample_Data\",\"ThirdFacilityAmt\":\"\",\"ThirdRepaymt\":\"\",\"ThirdIntrstRate\":\"\",\"ThirdMonthlyInstmt\":\"\",\"ThirdTermLoanAmt\":\"587.15\",\"ThirdInterest\":\"\",\"ThirdODLoan\":\"\",\"ThirdMRTA\":\"\",\"ThirdBankGuarantee\":\"\",\"ThirdLetterofCredit\":\"\",\"ThirdTrustReceipt\":\"\",\"ThirdOthers\":\"\",\"FourthTypeofFacility\":\"Sample4\",\"FourthFacilityAmt\":\"\",\"FourthRepaymt\":\"15\",\"FourthIntrstRate\":\"\",\"FourthMonthlyInstmt\":\"\",\"FourthTermLoanAmt\":\"\",\"FourthInterest\":\"21\",\"FourthODLoan\":\"\",\"FourthMRTA\":\"\",\"FourthBankGuarantee\":\"\",\"FourthLetterofCredit\":\"\",\"FourthTrustReceipt\":\"\",\"FourthOthers\":\"\",\"FifthTypeofFacility\":\"Sample 5\",\"FifthFacilityAmt\":\"\",\"FifthRepaymt\":\"\",\"FifthIntrstRate\":\"\",\"FifthMonthlyInstmt\":\"\",\"FifthTermLoanAmt\":\"\",\"FifthInterest\":\"10\",\"FifthODLoan\":\"\",\"FifthMRTA\":\"\",\"FifthBankGuarantee\":\"\",\"FifthLetterofCredit\":\"\",\"FifthTrustReceipt\":\"Test\",\"FifthOthers\":\"\"}}]";
		Log.e("pur_string", json_element);
		
		JSONObject valuesObject = null;
		JSONArray list = null;
	try {		
		list = new JSONArray(json_element);
		 valuesObject = list.getJSONObject(0);
		
			/*valuesObject.put("PurSPADate",spa_date.getText().toString());
			valuesObject.put("PurEntryOfPrivateCaveat", entry_private_caveat.getText().toString());
			valuesObject.put("PurWithOfPrivateCaveat", withdrawal_private_caveat.getText().toString());
			valuesObject.put("PurFirstName", name1.getText().toString());
			valuesObject.put("PurFirstID", brn_no1.getText().toString());
			valuesObject.put("PurFirstTaxNo", tax_no1.getText().toString());
			valuesObject.put("PurFirstContactNo", contact_no1.getText().toString());*/
			
			list.put(valuesObject);
			
			
	} catch (JSONException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
	}
	
	Log.v("texttt", list.toString());
		
	RequestParams params = new RequestParams();
	params.put("sJsonInput", list.toString());
	System.out.println("params");

	RestService.post(CONFIRM_BTN_REQUEST, params, new BaseJsonHttpResponseHandler<String>() {

		@Override
		public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
			// TODO Auto-generated method stub
			System.out.println(arg3);
			System.out.println("onFailure process case purchaser");
		}

		@Override
		public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
			// TODO Auto-generated method stub
			System.out.println("Add purchaser Confirmed");
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
				Intent iAddBack = new Intent(ProcessCasePurchaser.this, ProcessCaseVendor.class);
				startActivity(iAddBack);
				Toast.makeText(ProcessCasePurchaser.this, messageDisplay, Toast.LENGTH_SHORT).show();
			} else {
				Toast.makeText(ProcessCasePurchaser.this, messageDisplay, Toast.LENGTH_SHORT).show();

			}
		}

		@Override
		protected String parseResponse(String arg0, boolean arg1) throws Throwable {

			// Get Json response
			arrayResponse = new JSONArray(arg0);
			jsonResponse = arrayResponse.getJSONObject(0);

			System.out.println("Purchaser Add Response");
			System.out.println(arg0);
			return null;
		}
	});			
	}
	
	
	public void zoombtncor(String FullName , String MobileNum , String IDNum , String Category) throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("CompanyName", FullName);
		jsonObject.put("Address", "");
		jsonObject.put("RegNum", IDNum);
		jsonObject.put("Category", Category);
		params.put("sJsonInput", jsonObject.toString());

		RestService.post("SPA_CorporateSearch", params,
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
						System.out.println("ZoomCorp Dropdown Success Details ");
						System.out.println(arg2);

						try {

							arrayResponse = new JSONArray(arg2);
							
							JSONObject jres = arrayResponse.getJSONObject(0);
							
							System.out.println("zoom Corp Responce");
							System.out.println(jres);
							
							
							// Find Intent to call view property details
							Intent i = new Intent(ProcessCasePurchaser.this, CorporateActivity.class);
							
							// Send the property details in property UI through intent

							i.putExtra("CodeC_T", jres.getString("Code".toString()));							

							i.putExtra("DocEntryC_T", jres.getString("DocEntry".toString()));
							
							i.putExtra("CompNameC_T", jres.getString("CompName".toString()));
							

							i.putExtra("BRNNoC_T", jres.getString("BRNNo".toString()));
							

							i.putExtra("TaxNoC_T", jres.getString("TaxNo".toString()));
							

							i.putExtra("OfficeNoC_T", jres.getString("OfficeNo".toString()));
							
							i.putExtra("IDAddress1C_T", jres.getString("IDAddress1".toString()));
							
							i.putExtra("IDAddress2C_T", jres.getString("IDAddress2".toString()));
							

							i.putExtra("IDAddress3C_T", jres.getString("IDAddress3".toString()));
							
							i.putExtra("IDAddress4C_T", jres.getString("IDAddress4".toString()));
							

							i.putExtra("IDAddress5C_T", jres.getString("IDAddress5".toString()));
							

							i.putExtra("CorresAddr1C_T", jres.getString("CorresAddr1".toString()));
							
							i.putExtra("CorresAddr2C_T", jres.getString("CorresAddr2".toString()));
							
							
							i.putExtra("CorresAddr3C_T", jres.getString("CorresAddr3".toString()));
							

							i.putExtra("CorresAddr4C_T", jres.getString("CorresAddr4".toString()));
							

							i.putExtra("CorresAddr5C_T", jres.getString("CorresAddr5".toString()));
							

							i.putExtra("AddressToUseC_T", jres.getString("AddressToUse".toString()));
							
							
							i.putExtra("LastUpdatedOnC_T", jres.getString("LastUpdatedOn".toString()));
							

							startActivity(i);


							
						
							
							

							

							

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}

		

	public void zoombtnin(String FullName , String MobileNum , String IDNum , String Category) throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("FullName", FullName);
		jsonObject.put("MobileNum", MobileNum);
		jsonObject.put("IDNum", IDNum);
		jsonObject.put("Category", Category);
		params.put("sJsonInput", jsonObject.toString());

		RestService.post("SPA_IndividualSearch", params,
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
						System.out.println("Zoomindivujavl Dropdown Success Details ");
						System.out.println(arg2);

						try {

							arrayResponse = new JSONArray(arg2);
							
							JSONObject jres = arrayResponse.getJSONObject(0);
							
							System.out.println("zoom Responce");
							System.out.println(jres);
							
							
							// Find Intent to call view property details
							Intent i = new Intent(ProcessCasePurchaser.this, IndividualActivity.class);
							
							// Send the property details in property UI through intent

							i.putExtra("CODE_T", jres.getString("Code".toString()));							

							i.putExtra("DocEntry_T", jres.getString("DocEntry".toString()));
							
							i.putExtra("EmployeeName_T", jres.getString("EmployeeName".toString()));
							

							i.putExtra("Title_T", jres.getString("Title".toString()));
							

							i.putExtra("Gender_T", jres.getString("Gender".toString()));
							

							i.putExtra("IDNo1_T", jres.getString("IDNo1".toString()));
							
							i.putExtra("IDNo3_T", jres.getString("IDNo3".toString()));
							
							i.putExtra("TaxNo_T", jres.getString("TaxNo".toString()));
							

							i.putExtra("MobileNo_T", jres.getString("MobileNo".toString()));
							
							i.putExtra("Telephone_T", jres.getString("Telephone".toString()));
							

							i.putExtra("OfficeNo_T", jres.getString("OfficeNo".toString()));
							

							i.putExtra("IDAddress1_T", jres.getString("IDAddress1".toString()));
							
							i.putExtra("IDAddress2_T", jres.getString("IDAddress2".toString()));
							
							
							i.putExtra("IDAddress3_T", jres.getString("IDAddress3".toString()));
							

							i.putExtra("IDAddress4_T", jres.getString("IDAddress4".toString()));
							

							i.putExtra("IDAddress5_T", jres.getString("IDAddress5".toString()));
							

							i.putExtra("CorresAddr1_T", jres.getString("CorresAddr1".toString()));
							
							
							i.putExtra("CorresAddr2_T", jres.getString("CorresAddr2".toString()));
							

							i.putExtra("CorresAddr3_T", jres.getString("CorresAddr3".toString()));
							
							
							i.putExtra("CorresAddr4_T", jres.getString("CorresAddr4".toString()));
							

							i.putExtra("CorresAddr5_T", jres.getString("CorresAddr5".toString()));
							

							i.putExtra("AddressToUse_T", jres.getString("AddressToUse".toString()));
							

							i.putExtra("LastUpdatedOn_T", jres.getString("LastUpdatedOn".toString()));
							
							i.putExtra("FrontIC_T", jres.getString("FrontIC".toString()));
							i.putExtra("BackIC_T", jres.getString("BackIC".toString()));
							
							

							startActivity(i);


							
						
							
							

							

							

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}

		
	
public boolean dispatchTouchEvent(MotionEvent ev) {
		InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
		imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
		return super.dispatchTouchEvent(ev);

	}
}
