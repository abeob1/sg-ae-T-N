package com.abeo.tia.noordin;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Locale;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.app.DatePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemSelectedListener;
import android.view.View;

public class ProcessCaseProperty extends BaseActivity implements OnClickListener{
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	
	EditText title_no, certified_plan_no, no_lot, previously_knowas, state, area, bandar_mukin, 
	govn_survey_plan, lot_area,sq_meter, developer, project, dev_license_no, dev_solicitor, soli_loc, 
	title_search, date_submit_consent, date_receive_consent, date14_a, date_of_return,
	bank_name, branch, pa_name, presentation_no, existing_charge_refs, receipt_no, 
	receipt_date, developer_solicitor2, dev_solicitor_loc2, bank_name2, branch2, pa_name2, presentation_no2,
	existing_charge_refs2, receipt_no2, receipt_date2, purchase_price, adjudicated_value, vendor_prev,
	deposit, balance_pur_price, loan_amt, loan_case_no, differential_sum, redemption_amt, redemption_date,
	defict_rdmt_sum;
	
	//header edittext
	EditText case_type, case_file_no, file_open_date;
	
	CheckBox QryGroup13;
	
	//datepicker
		String myFormat = "MM/dd/yyyy";
		Calendar myCalendar = Calendar.getInstance();
	
	Button confirm_btn, btn_details, btn_purchaser, btn_vendor, btn_loan_principle, btn_loan_subsidairy, btn_process,walkin;
	
	Button purchaser_btn, vendor_btn, property_btn, loan_principal_btn,
	loan_subsidary_btn, process_btn,
	past_sec, next_sec, add_optional, view_file, process_step, btnClosePopup,details_btn;
	
	private final String METHOD_PROCESS_CASE_DETAILS = "SPA_ProcessCase_GetDataFromOCRD";

	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;
	JSONObject jsonResponseconfirm = null;

	// spinner declaration
	Spinner spinner_case_status, spinner_kiv,Receipttype,Subtype,spinnerpropertySTATE;

	TextView ID, TEXT;
	String caseValue_id = "", titleValue = "", casetype = "",
			casetype_value = "",QryGroup14="",QryGroup15="",QryGroup16="",qryval,Scase_status, Skiv,SReceipttype,SSubtype,statevalue;
	
	// Get Project value fromapi
		private final String GET_SPINNER_VALUES = "SPA_GetValidValues";
		ArrayList<HashMap<String, String>> jsonArraylist = null,jsonliststate=null;
		String id, name;
		SimpleAdapter sAdapPROJ, sAdaparea= null;
		String GET_TYPE_SPINNER = "SPA_ProcessCase_GetIDType",stateval_id,stateval;
		
		//Confirm btn request URL
		String CONFIRM_BTN_REQUEST = "SPA_ProcessCase_UpdateCaseTabDetails";
		
		String messageDisplay = "", StatusResult = "",Receipttype_val="";
		
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_process_case_property1);
		
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
				
				QryGroup13 = (CheckBox) findViewById(R.id.PropetyCharged);
				
				//Edittext declaration
				case_type = (EditText) findViewById(R.id.editText_ProCaseCaseType);
				case_file_no = (EditText) findViewById(R.id.editText_ProCaseCaseFileNo);
				file_open_date = (EditText) findViewById(R.id.editText_ProCaseFileOpenDate);
				title_no = (EditText) findViewById(R.id.editText_ProCaseProperTytitleNo);
				//certified_plan_no = (EditText) findViewById(R.id.editText_ProCasePropertyCertifiedPlanNo);
				no_lot = (EditText) findViewById(R.id.editText_PropertyNoLot);
				previously_knowas = (EditText) findViewById(R.id.editText_PropertyPreviouslyKnownAs);
				//state = (EditText) findViewById(R.id.editText_ProCasePropertyDaerahState);
				area = (EditText) findViewById(R.id.editText_ProCasePropertyNageriArea);
				bandar_mukin = (EditText) findViewById(R.id.editText_ProCasePropertyBandarPekanMukin);
				//govn_survey_plan = (EditText) findViewById(R.id.editText_ProCasePropertyGovn);
				lot_area = (EditText) findViewById(R.id.editText_ProCasePropertyLotArea);
				//sq_meter = (EditText) findViewById(R.id.editText_PropertySqMeter);
				developer = (EditText) findViewById(R.id.spinner_ProCasePropertyDevelopoer);
				project = (EditText) findViewById(R.id.spinner_ProCasePropertyProjectDropdown);
				dev_license_no = (EditText) findViewById(R.id.editText_ProCasePropertyDevLicense);
				dev_solicitor = (EditText) findViewById(R.id.spinner_ProCasePropertySolicitor);
				soli_loc = (EditText) findViewById(R.id.editText_ProCasePropertySolicitorLoc);
				title_search = (EditText) findViewById(R.id.spinner_ProCasePropertyTitleSearchDate);
				date_submit_consent = (EditText) findViewById(R.id.spinner_ProCasePropertyProjectDSCT);
				date_receive_consent = (EditText) findViewById(R.id.editText_ProCasePropertyDRCT);
				date14_a = (EditText) findViewById(R.id.spinner_ProCaseProperty14ADate);
				date_of_return = (EditText) findViewById(R.id.editText_ProCasePropertyDRTLR);
				bank_name = (EditText) findViewById(R.id.spinner_ProCasePropertyProjectBank);
				branch  = (EditText) findViewById(R.id.editText_ProCasePropertyBranch);
				pa_name = (EditText) findViewById(R.id.editText_ProCasePropertyPAName);
				presentation_no = (EditText) findViewById(R.id.editText_ProCasePropertyPresentaionNo);
				existing_charge_refs = (EditText) findViewById(R.id.spinner_ProCasePropertyECR);
				receipt_no = (EditText) findViewById(R.id.editText_ProCasePropertReceiptNo);
				receipt_date = (EditText) findViewById(R.id.editText_ProCasePropertyReceiptDate);
				/*developer_solicitor2 = (EditText) findViewById(R.id.spinner_ProCase2PropertySolicitor);
				dev_solicitor_loc2 = (EditText) findViewById(R.id.editText_ProCase2PropertySolicitorLoc);
				bank_name2 = (EditText) findViewById(R.id.spinner_ProCase2PropertyProjectBank);
				branch2 = (EditText) findViewById(R.id.editText_ProCase2PropertyBranch);
				pa_name2 = (EditText) findViewById(R.id.editText_ProCase2PropertyPAName);
				presentation_no2 = (EditText) findViewById(R.id.editText_ProCase2PropertyPresentaionNo);
				existing_charge_refs2 = (EditText) findViewById(R.id.spinner_ProCase2PropertyECR);
				receipt_no2 = (EditText) findViewById(R.id.editText_ProCase2PropertReceiptNo);
				receipt_date2 = (EditText) findViewById(R.id.editText_ProCase2PropertyReceiptDate);*/
				purchase_price = (EditText) findViewById(R.id.editText_ProCase2PropertyPurchasePrice);
				adjudicated_value = (EditText) findViewById(R.id.editText_ProCase2PropertyAdjudicated);
				vendor_prev = (EditText) findViewById(R.id.spinner_ProCase2PropertyVNDR);
				deposit = (EditText) findViewById(R.id.editText_ProCase2PropertyDeposite);
				balance_pur_price = (EditText) findViewById(R.id.editText_ProCase2PropertBalance);
				loan_amt = (EditText) findViewById(R.id.editText_ProCase2PropertyLoanAmount);
				loan_case_no = (EditText) findViewById(R.id.spinner_ProCase2PropertyLoanCase);
				differential_sum = (EditText) findViewById(R.id.spinner_ProCase2PropertyDifferential);
				redemption_amt = (EditText) findViewById(R.id.editText_ProCase2Redemption);
				redemption_date = (EditText) findViewById(R.id.spinner_ProCase2PropertyRedemptionADate);
				defict_rdmt_sum = (EditText) findViewById(R.id.editText_ProCase2PropertyDRTLR);
				
				confirm_btn = (Button) findViewById(R.id.button_ProCasePropertyConfirm);
				
				confirm_btn.setOnClickListener(this);
				
				
				//buttons initialization
				details_btn = (Button) findViewById(R.id.button_ProCasePropertyDetails);
		        purchaser_btn = (Button) findViewById(R.id.button_ProCasePropertyPurchaser);
		        vendor_btn = (Button) findViewById(R.id.button_ProCasePropertyVendor);
		        property_btn = (Button) findViewById(R.id.button_ProCasePropertyProperty);
		        loan_principal_btn = (Button) findViewById(R.id.button_ProCasePropertyPrincipal);
		        loan_subsidary_btn = (Button) findViewById(R.id.button_ProCasePropertyLoanSubsidiary);
		        process_btn = (Button) findViewById(R.id.button_ProCasePropertyProcess);
		        walkin = (Button) findViewById(R.id.button_ProCasePropertyWalkin);
		        
		        details_btn.setOnClickListener(this);
		        purchaser_btn.setOnClickListener(this);
		        vendor_btn.setOnClickListener(this);
		        property_btn.setOnClickListener(this);
		        loan_principal_btn.setOnClickListener(this);
		        loan_subsidary_btn.setOnClickListener(this);
		        process_btn.setOnClickListener(this);
		        walkin.setOnClickListener(this);
		        
		        title_search.setOnClickListener(this);
				date_submit_consent.setOnClickListener(this);
				date_receive_consent.setOnClickListener(this); 
				date14_a.setOnClickListener(this);
				date_of_return.setOnClickListener(this);
				receipt_date.setOnClickListener(this);
				redemption_date.setOnClickListener(this);
				
				// spinners initialization
				spinner_case_status = (Spinner) findViewById(R.id.case_status);
				spinner_kiv = (Spinner) findViewById(R.id.spinner_ProcessCase1KIV);
				Receipttype = (Spinner) findViewById(R.id.spinner_Receipttype);
				Subtype = (Spinner) findViewById(R.id.spinner_SubType);
				spinnerpropertySTATE = (Spinner)  findViewById(R.id.state);
				
				
				// Spinner click listener
				spinnerpropertySTATE.setOnItemSelectedListener(new OnItemSelectedListener() {

							@Override
							public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
								ID = (TextView) view.findViewById(R.id.Id);
								stateval_id = ID.getText().toString();
								TEXT = (TextView) view.findViewById(R.id.Name);
								stateval = TEXT.getText().toString();

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
				
				
				
				Subtype.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent,
							View view, int position, long id) {
						ID = (TextView) view.findViewById(R.id.Id);
						caseValue_id = ID.getText().toString();
						TEXT = (TextView) view.findViewById(R.id.Name);
						qryval = TEXT.getText().toString();
						
						if(qryval.equals("Individual Title"))
						{
							QryGroup14 = "Y";
							QryGroup16 = "";
							QryGroup15 = "";
						}
						if(qryval.equals("Strata Title"))
						{
							QryGroup15 = "Y";
							QryGroup16 = "";
							QryGroup14 = "";
						}
						if(qryval.equals("Master Title"))
						{
							QryGroup16 = "Y";
							QryGroup14 = "";
							QryGroup15 = "";
						}
							
					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
		
				
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
				Receipttype
						.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

							@Override
							public void onItemSelected(AdapterView<?> parent,
									View view, int position, long id) {
								ID = (TextView) view.findViewById(R.id.Id);
								casetype = ID.getText().toString();
								TEXT = (TextView) view.findViewById(R.id.Name);
								Receipttype_val = TEXT.getText().toString();
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
				try {
					setvaluestoUI();					
				} catch (JSONException e) {
					e.printStackTrace();
				}
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
	public void dropdownRT() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "OCRD");
		jsonObject.put("FieldName", "RECEIPT_TYPE");
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
									ProcessCaseProperty.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							Receipttype.setAdapter(sAdapPROJ);

							
							  for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(SReceipttype)) {
								  Receipttype.setSelection(j); break; } }
							 

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}
	
	public void Subtype() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		//JSONObject jsonObject = new JSONObject();
		//jsonObject.put("TableName", "OCRD");
		//jsonObject.put("FieldName", "KIVSTATUS");
		//params.put("sJsonInput", jsonObject.toString());

		RestService.post("SPA_ProcessCase_TitleSubType", params,
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
									ProcessCaseProperty.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							Subtype.setAdapter(sAdapPROJ);

							
							  for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(
									  SSubtype)) {
								  Subtype.setSelection(j); break; } }
							 

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}
	

	public void dropdownState() throws JSONException {

		RequestParams params = null;
		params = new RequestParams();
		
		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "OCRD");
		jsonObject.put("FieldName", "STATE");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(GET_SPINNER_VALUES, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("State Dropdown Success Details ");
				System.out.println(arg2);

				try {

					arrayResponse = new JSONArray(arg2);
					// Create new list
					jsonliststate = new ArrayList<HashMap<String, String>>();

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

						jsonliststate.add(proList);
						System.out.println("JSON STATE LIST");
						System.out.println(jsonliststate);
					}
					// Spinner set Array Data in Drop down

					sAdaparea = new SimpleAdapter(ProcessCaseProperty.this, jsonliststate, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertySTATE.setAdapter(sAdaparea);

					for (int j = 0; j < jsonliststate.size(); j++) {
						if (jsonliststate.get(j).get("Id_T").equals(statevalue)) {
							spinnerpropertySTATE.setSelection(j);
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

				System.out.println("State Dropdown Details parse Response");
				System.out.println(arg0);
				return null;
			}
		});

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
									ProcessCaseProperty.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_kiv.setAdapter(sAdapPROJ);

							
							  for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(Skiv)) {
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
									ProcessCaseProperty.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_case_status.setAdapter(sAdapPROJ);

							
							  for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(
									  Scase_status)) {
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
				

				JSONObject obj1 = jsonobject.getJSONObject("Property");

				title_no.setText(obj1.getString("TitleType"));
				//certified_plan_no.setText(obj1.getString("CertifiedPlanNo"));
				no_lot.setText(obj1.getString("LotNo"));
				previously_knowas.setText(obj1.getString("PreviouslyKnownAs"));
				statevalue = obj1.getString("State");
				//state.setText(obj1.getString("State"));
				area.setText(obj1.getString("Area"));
				bandar_mukin.setText(obj1.getString("BPM"));
				//govn_survey_plan.setText(obj1.getString("GovSurvyPlan"));
				lot_area.setText(obj1.getString("LotArea"));
				developer.setText(obj1.getString("Developer"));
				project.setText(obj1.getString("Project"));
				dev_license_no.setText(obj1.getString("DevLicenseNo"));
				dev_solicitor.setText(obj1.getString("DevSolicitor"));
				soli_loc.setText(obj1.getString("DevSoliLoc"));
				title_search.setText(obj1.getString("TitleSearchDate"));
				date_submit_consent.setText(obj1.getString("DSCTransfer"));
				date_receive_consent.setText(obj1.getString("DRCTransfer"));
				date14_a.setText(obj1.optString("FourteenADate"));
				date_of_return.setText(obj1.getString("DRTLRegistry"));
				//property charged
				bank_name.setText(obj1.getString("BankName"));
				branch.setText(obj1.getString("Branch"));
				pa_name.setText(obj1.getString("PAName"));
				presentation_no.setText(obj1.getString("PresentationNo"));
				existing_charge_refs.setText(obj1.getString("ExistChargeRef"));
				//receipt type
				receipt_no.setText(obj1.getString("ReceiptNo"));
				receipt_date.setText(obj1.getString("ReceiptDate"));
				//developer_solicitor2.setText(obj1.getString("PurchasePrice"));
				//dev_solicitor_loc2.setText(obj1.getString(""));	
				purchase_price.setText(obj1.getString("PurchasePrice"));
				adjudicated_value.setText(obj1.getString("AdjValue"));
				vendor_prev.setText(obj1.getString("VndrPrevSPAValue"));
				deposit.setText(obj1.optString("Deposit"));
				balance_pur_price.setText(obj1.getString("BalPurPrice"));
				loan_amt.setText(obj1.getString("LoanAmount"));
				loan_case_no.setText(obj1.getString("LoanCaseNo"));
				differential_sum.setText(obj1.getString("DiffSum"));
				redemption_amt.setText(obj1.getString("RedAmt"));
				redemption_date.setText(obj1.getString("RedDate"));
				defict_rdmt_sum.setText(obj1.getString("DefRdmptSum"));
				if(obj1.getString("PropertyCharged").equals("Y"))
					QryGroup13.setChecked(true);
				if(obj1.getString("PropertyCharged").equals("N"))
					QryGroup13.setChecked(false);
				
				
				if(obj1.getString("QryGroup14").equals("Y"))
					SSubtype = "Individual Title";
				if(obj1.getString("QryGroup15").equals("Y"))
					SSubtype = "Strata Title";
				if(obj1.getString("QryGroup16").equals("Y"))
					SSubtype = "Master Title";
				Skiv = jsonobject.getString("KIV");
				SReceipttype = obj1.getString("ReceiptType");
				
				dropdownstatus();
				dropdownKIV();
				dropdownRT();
				dropdownState();
				Subtype();
				
				
			}
			System.out.println(jObj);
		} catch (JSONException e) {
			e.printStackTrace();
		}
	}
	
	private void confirm_values() throws JSONException {
		System.out.print(jsonResponseconfirm);
		// TODO Auto-generated method stub 
//			String json_element = "[{\"Case\":\"1500000001\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"CaseStatus\":\"OPEN\",\"FileOpenDate\":\"10/10/2015 12:00:00 AM\",\"CaseFileNo\":\"JJ/1500000001/\",\"KIV\":\"\",\"TabId\":\"2\",\"Details\":{\"LA\":\"\",\"MANAGER\":\"\",\"InCharge\":\"\",\"CustomerService\":\"\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"FileLocation\":\"\",\"FileClosedDate\":\"\",\"VendAcqDt\":\"\",\"CompanyBuisnessSearch\":\"\",\"BankWindingSearch\":\"\"},\"Purchaser\":{\"PurRepresentedByFirm\":\"N\",\"PurlawyerRepresented\":\"N\",\"PurSPADate\":\"1/1/1900 12:00:00 AM\",\"PurEntryOfPrivateCaveat\":\"Chase\",\"PurWithOfPrivateCaveat\":\"Rane\",\"PurFirstName\":\"Name1\",\"PurFirstID\":\"Id1\",\"PurFirstTaxNo\":\"Tax1\",\"PurFirstContactNo\":\"9784561233\",\"PurFirstType\":\"CORPORATE\",\"PurSecName\":\"Name2\",\"PurSecID\":\"Id2\",\"PurSecTaxNo\":\"Tax2\",\"PurSecContactNo\":\"9784561234\",\"PurSecType\":\"INDIVIDUAL\",\"PurThirdName\":\"Name3\",\"PurThirdID\":\"Id3\",\"PurThirdTaxNo\":\"Tax3\",\"PurThirdContactNo\":\"9784561234\",\"PurThirdType\":\"INDIVIDUAL\",\"PurFourthName\":\"Name4\",\"PurFourthID\":\"Id4\",\"PurFourthTaxNo\":\"Tax4\",\"PurFourthContactNo\":\"9784561235\",\"PurFourthType\":\"INDIVIDUAL\"},\"Vendor\":{\"VndrRepresentedByFirm\":\"N\",\"VndrlawyerRepresented\":\"Y\",\"VndrReqDevConsent\":\"Name\",\"VndrReceiveDevConsent\":\"Sam\",\"VndrFirstName\":\"Name1\",\"VndrFirstID\":\"Id1\",\"VndrFirstTaxNo\":\"Tax1\",\"VndrFirstContactNo\":\"9784561233\",\"VndrFirstType\":\"CORPORATE\",\"VndrSecName\":\"Name2\",\"VndrSecID\":\"Id2\",\"VndrSecTaxNo\":\"Tax2\",\"VndrSecContactNo\":\"9784561234\",\"VndrSecType\":\"INDIVIDUAL\",\"VndrThirdName\":\"Name3\",\"VndrThirdID\":\"Id3\",\"VndrThirdTaxNo\":\"Tax3\",\"VndrThirdContactNo\":\"9784561234\",\"VndrThirdType\":\"INDIVIDUAL\",\"VndrFourthName\":\"Name4\",\"VndrFourthID\":\"Id4\",\"VndrFourthTaxNo\":\"Tax4\",\"VndrFourthContactNo\":\"9784561235\",\"VndrFourthType\":\"INDIVIDUAL\"},\"Property\":{\"TitleType\":\"KEKAL\",\"CertifiedPlanNo\":\"\",\"LotNo\":\"\",\"PreviouslyKnownAs\":\"\",\"State\":\"\",\"Area\":\"\",\"BPM\":\"\",\"GovSurvyPlan\":\"\",\"LotArea\":\"12345\",\"Developer\":\"\",\"Project\":\"\",\"DevLicenseNo\":\"\",\"DevSolicitor\":\"\",\"DevSoliLoc\":\"\",\"TitleSearchDate\":\"\",\"DSCTransfer\":\"\",\"DRCTransfer\":\"\",\"FourteenADate\":\"\",\"DRTLRegistry\":\"\",\"PropertyCharged\":\"\",\"BankName\":\"\",\"Branch\":\"\",\"PAName\":\"\",\"PresentationNo\":\"\",\"ExistChargeRef\":\"\",\"ReceiptType\":\"\",\"ReceiptNo\":\"\",\"ReceiptDate\":\"\",\"PurchasePrice\":\"1452\",\"AdjValue\":\"\",\"VndrPrevSPAValue\":\"\",\"Deposit\":\"\",\"BalPurPrice\":\"\",\"LoanAmount\":\"\",\"LoanCaseNo\":\"1234\",\"DiffSum\":\"\",\"RedAmt\":\"\",\"RedDate\":\"\",\"DefRdmptSum\":\"\"},\"LoanPrinciple\":{\"ReqRedStatement\":\"\",\"RedStmtDate\":\"\",\"RedPayDate\":\"\",\"RepByFirm\":\"\",\"LoanCaseNo\":\"1234\",\"Project\":\"\",\"MasterBankName\":\"\",\"BranchName\":\"\",\"Address\":\"\",\"PAName\":\"\",\"BankRef\":\"\",\"BankInsDate\":\"2015/10/11\",\"LOFDate\":\"\",\"BankSolicitor\":\"\",\"SoliLoc\":\"\",\"SoliRef\":\"\",\"TypeofLoan\":\"\",\"TypeofFacility\":\"\",\"FacilityAmt\":\"\",\"Repaymt\":\"\",\"IntrstRate\":\"\",\"MonthlyInstmt\":\"\",\"TermLoanAmt\":\"\",\"Interest\":\"\",\"ODLoan\":\"\",\"MRTA\":\"\",\"BankGuarantee\":\"\",\"LetterofCredit\":\"\",\"TrustReceipt\":\"\",\"Others\":\"\",\"LoanDet1\":\"\",\"LoanDet2\":\"\",\"LoanDet3\":\"\",\"LoanDet4\":\"\",\"LoanDet5\":\"\"},\"LoanSubsidary\":{\"LoanDocForBankExe\":\"Exe 1\",\"FaciAgreeDate\":\"\",\"LoanDocRetFromBank\":\"\",\"DischargeofCharge\":\"\",\"FirstTypeofFacility\":\"\",\"FirstFacilityAmt\":\"\",\"FirstRepaymt\":\"\",\"FirstIntrstRate\":\"\",\"FirstMonthlyInstmt\":\"1500\",\"FirstTermLoanAmt\":\"\",\"FirstInterest\":\"\",\"FirstODLoan\":\"\",\"FirstMRTA\":\"\",\"FirstBankGuarantee\":\"\",\"FirstLetterofCredit\":\"\",\"FirstTrustReceipt\":\"\",\"FirstOthers\":\"Sample\",\"SecTypeofFacility\":\"\",\"SecFacilityAmt\":\"\",\"SecRepaymt\":\"\",\"SecIntrstRate\":\"12%\",\"SecMonthlyInstmt\":\"\",\"SecTermLoanAmt\":\"\",\"SecInterest\":\"\",\"SecODLoan\":\"\",\"SecMRTA\":\"\",\"SecBankGuarantee\":\"\",\"SecLetterofCredit\":\"\",\"SecTrustReceipt\":\"45A\",\"SecOthers\":\"\",\"ThirdTypeofFacility\":\"Sample_Data\",\"ThirdFacilityAmt\":\"\",\"ThirdRepaymt\":\"\",\"ThirdIntrstRate\":\"\",\"ThirdMonthlyInstmt\":\"\",\"ThirdTermLoanAmt\":\"587.15\",\"ThirdInterest\":\"\",\"ThirdODLoan\":\"\",\"ThirdMRTA\":\"\",\"ThirdBankGuarantee\":\"\",\"ThirdLetterofCredit\":\"\",\"ThirdTrustReceipt\":\"\",\"ThirdOthers\":\"\",\"FourthTypeofFacility\":\"Sample4\",\"FourthFacilityAmt\":\"\",\"FourthRepaymt\":\"15\",\"FourthIntrstRate\":\"\",\"FourthMonthlyInstmt\":\"\",\"FourthTermLoanAmt\":\"\",\"FourthInterest\":\"21\",\"FourthODLoan\":\"\",\"FourthMRTA\":\"\",\"FourthBankGuarantee\":\"\",\"FourthLetterofCredit\":\"\",\"FourthTrustReceipt\":\"\",\"FourthOthers\":\"\",\"FifthTypeofFacility\":\"Sample 5\",\"FifthFacilityAmt\":\"\",\"FifthRepaymt\":\"\",\"FifthIntrstRate\":\"\",\"FifthMonthlyInstmt\":\"\",\"FifthTermLoanAmt\":\"\",\"FifthInterest\":\"10\",\"FifthODLoan\":\"\",\"FifthMRTA\":\"\",\"FifthBankGuarantee\":\"\",\"FifthLetterofCredit\":\"\",\"FifthTrustReceipt\":\"Test\",\"FifthOthers\":\"\"}}]";
		String json_element = "[{\"Case\":" + "\"" + jsonResponseconfirm.get("Case").toString() + "\",\"CaseType\":" + "\"" + jsonResponseconfirm.get("CaseType").toString() + "\",\"CaseStatus\":\"OPEN\",\"FileOpenDate\":\"10/10/2015 12:00:00 AM\",\"CaseFileNo\":" + "\"" + jsonResponseconfirm.get("CaseFileNo").toString() + "\",\"KIV\":\"\",\"TabId\":\"4\",\"Details\":{\"LA\":\"\",\"MANAGER\":\"\",\"InCharge\":\"\",\"CustomerService\":\"\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"FileLocation\":\"\",\"FileClosedDate\":\"\",\"VendAcqDt\":\"\",\"CompanyBuisnessSearch\":\"\",\"BankWindingSearch\":\"\"},\"Purchaser\":{\"PurRepresentedByFirm\":\"N\",\"PurlawyerRepresented\":\"N\",\"PurSPADate\":\"1/1/1900 12:00:00 AM\",\"PurEntryOfPrivateCaveat\":\"Chase\",\"PurWithOfPrivateCaveat\":\"Rane\",\"PurFirstName\":\"Name1\",\"PurFirstID\":\"Id1\",\"PurFirstTaxNo\":\"Tax1\",\"PurFirstContactNo\":\"9784561233\",\"PurFirstType\":\"CORPORATE\",\"PurSecName\":\"Name2\",\"PurSecID\":\"Id2\",\"PurSecTaxNo\":\"Tax2\",\"PurSecContactNo\":\"9784561234\",\"PurSecType\":\"INDIVIDUAL\",\"PurThirdName\":\"Name3\",\"PurThirdID\":\"Id3\",\"PurThirdTaxNo\":\"Tax3\",\"PurThirdContactNo\":\"9784561234\",\"PurThirdType\":\"INDIVIDUAL\",\"PurFourthName\":\"Name4\",\"PurFourthID\":\"Id4\",\"PurFourthTaxNo\":\"Tax4\",\"PurFourthContactNo\":\"9784561235\",\"PurFourthType\":\"INDIVIDUAL\"},\"Vendor\":{\"VndrRepresentedByFirm\":\"N\",\"VndrlawyerRepresented\":\"Y\",\"VndrReqDevConsent\":\"Name\",\"VndrReceiveDevConsent\":\"Sam\",\"VndrFirstName\":\"Name1\",\"VndrFirstID\":\"Id1\",\"VndrFirstTaxNo\":\"Tax1\",\"VndrFirstContactNo\":\"9784561233\",\"VndrFirstType\":\"CORPORATE\",\"VndrSecName\":\"Name2\",\"VndrSecID\":\"Id2\",\"VndrSecTaxNo\":\"Tax2\",\"VndrSecContactNo\":\"9784561234\",\"VndrSecType\":\"INDIVIDUAL\",\"VndrThirdName\":\"Name3\",\"VndrThirdID\":\"Id3\",\"VndrThirdTaxNo\":\"Tax3\",\"VndrThirdContactNo\":\"9784561234\",\"VndrThirdType\":\"INDIVIDUAL\",\"VndrFourthName\":\"Name4\",\"VndrFourthID\":\"Id4\",\"VndrFourthTaxNo\":\"Tax4\",\"VndrFourthContactNo\":\"9784561235\",\"VndrFourthType\":\"INDIVIDUAL\"},\"Property\":{\"" + "TitleType" + "\":" + "\"" + title_no.getText().toString() + "\",\"PropertyCharged\":" + "\"" + QryGroup13.isChecked() + "\",\"QryGroup14\":" + "\"" + QryGroup14 + "\",\"QryGroup15\":" + "\"" + QryGroup15 + "\",\"QryGroup16\":" + "\"" + QryGroup16 + "\",\"" + "CertifiedPlanNo" + "\":" + "\"\",\"" + "LotNo" + "\":" + "\"" + no_lot.getText().toString() + "\",\"" + "PreviouslyKnownAs" + "\":" + "\"" + previously_knowas.getText().toString() + "\",\"" + "State" + "\":" + "\"" + stateval + "\",\"" + "Area" + "\":" + "\"" + area.getText().toString() + "\",\"" + "ReceiptNo" + "\": " + "\"" + receipt_no.getText().toString() + "\",\"" + "BPM" + "\":" + "\"" + bandar_mukin.getText().toString() + "\",\"" + "GovSurvyPlan" + "\":" + "\"\",\"" + "LotArea" + "\":" + "\"" + lot_area.getText().toString() + "\",\"" + "Developer" + "\":" + "\"" + developer.getText().toString() + "\",\"" + "Project" + "\":" + "\"" + project.getText().toString() + "\",\"" + "DevLicenseNo" + "\":" + "\"" + dev_license_no.getText().toString() + "\",\"" + "DevSolicitor" + "\":" + "\"" + dev_solicitor.getText().toString() + "\",\"" + "DevSoliLoc" + "\":" + "\"" + soli_loc.getText().toString() + "\",\"" + "TitleSearchDate" + "\":" + "\"" + title_search.getText().toString() + "\",\"" + "DSCTransfer" + "\":" + "\"" + date_submit_consent.getText().toString() + "\",\"" + "DRCTransfer" + "\":" + "\"" + date_receive_consent.getText().toString() + "\",\"" + "FourteenADate" + "\":" + "\"" + date14_a.getText().toString() + "\",\"" + "DRTLRegistry" + "\":" + "\"" + date_of_return.getText().toString() + "\",\"" + "BankName" + "\":" + "\"" + bank_name.getText().toString() + "\",\"" + "Branch" + "\":" + "\"" + branch.getText().toString() + "\",\"" + "PAName" + "\":" + "\"" + pa_name.getText().toString() + "\",\"" + "PresentationNo" + "\":" + "\"" + presentation_no.getText().toString() + "\",\"" + "ExistChargeRef" + "\":" + "\"" + existing_charge_refs.getText().toString() + "\",\"ReceiptType\":"+"\""+Receipttype_val+"\",\"ReceiptDate" + "\":" + "\"" + receipt_date.getText().toString() + "\",\"" + "PurchasePrice" + "\":" + "\"" + purchase_price.getText().toString() + "\",\"" + "AdjValue" + "\":" + "\"" + adjudicated_value.getText().toString() + "\",\"" + "VndrPrevSPAValue" + "\":" + "\"" + vendor_prev.getText().toString() + "\",\"" + "Deposit" + "\":" + "\"" + deposit.getText().toString() + "\",\"" + "BalPurPrice" + "\":" + "\"" + balance_pur_price.getText().toString() + "\",\"" + "LoanAmount" + "\":" + "\"" + loan_amt.getText().toString() + "\",\"" + "LoanCaseNo" + "\":" + "\"" + loan_case_no.getText().toString() + "\",\"" + "DiffSum" + "\":" + "\"" + differential_sum.getText().toString() + "\",\"" + "RedAmt" + "\":" + "\"" + redemption_amt.getText().toString() + "\",\"" + "RedDate" + "\":" + "\"" + redemption_date.getText().toString() + "\",\"" + "DefRdmptSum" + "\":" + "\"" + defict_rdmt_sum.getText().toString() + "\"},\"LoanPrinciple\":{\"ReqRedStatement\":\"\",\"RedStmtDate\":\"\",\"RedPayDate\":\"\",\"RepByFirm\":\"\",\"LoanCaseNo\":\"1234\",\"Project\":\"\",\"MasterBankName\":\"\",\"BranchName\":\"\",\"Address\":\"\",\"PAName\":\"\",\"BankRef\":\"\",\"BankInsDate\":\"2015/10/11\",\"LOFDate\":\"\",\"BankSolicitor\":\"\",\"SoliLoc\":\"\",\"SoliRef\":\"\",\"TypeofLoan\":\"\",\"TypeofFacility\":\"\",\"FacilityAmt\":\"\",\"Repaymt\":\"\",\"IntrstRate\":\"\",\"MonthlyInstmt\":\"\",\"TermLoanAmt\":\"\",\"Interest\":\"\",\"ODLoan\":\"\",\"MRTA\":\"\",\"BankGuarantee\":\"\",\"LetterofCredit\":\"\",\"TrustReceipt\":\"\",\"Others\":\"\",\"LoanDet1\":\"\",\"LoanDet2\":\"\",\"LoanDet3\":\"\",\"LoanDet4\":\"\",\"LoanDet5\":\"\"},\"LoanSubsidary\":{\"LoanDocForBankExe\":\"Exe 1\",\"FaciAgreeDate\":\"\",\"LoanDocRetFromBank\":\"\",\"DischargeofCharge\":\"\",\"FirstTypeofFacility\":\"\",\"FirstFacilityAmt\":\"\",\"FirstRepaymt\":\"\",\"FirstIntrstRate\":\"\",\"FirstMonthlyInstmt\":\"1500\",\"FirstTermLoanAmt\":\"\",\"FirstInterest\":\"\",\"FirstODLoan\":\"\",\"FirstMRTA\":\"\",\"FirstBankGuarantee\":\"\",\"FirstLetterofCredit\":\"\",\"FirstTrustReceipt\":\"\",\"FirstOthers\":\"Sample\",\"SecTypeofFacility\":\"\",\"SecFacilityAmt\":\"\",\"SecRepaymt\":\"\",\"SecIntrstRate\":\"12%\",\"SecMonthlyInstmt\":\"\",\"SecTermLoanAmt\":\"\",\"SecInterest\":\"\",\"SecODLoan\":\"\",\"SecMRTA\":\"\",\"SecBankGuarantee\":\"\",\"SecLetterofCredit\":\"\",\"SecTrustReceipt\":\"45A\",\"SecOthers\":\"\",\"ThirdTypeofFacility\":\"Sample_Data\",\"ThirdFacilityAmt\":\"\",\"ThirdRepaymt\":\"\",\"ThirdIntrstRate\":\"\",\"ThirdMonthlyInstmt\":\"\",\"ThirdTermLoanAmt\":\"587.15\",\"ThirdInterest\":\"\",\"ThirdODLoan\":\"\",\"ThirdMRTA\":\"\",\"ThirdBankGuarantee\":\"\",\"ThirdLetterofCredit\":\"\",\"ThirdTrustReceipt\":\"\",\"ThirdOthers\":\"\",\"FourthTypeofFacility\":\"Sample4\",\"FourthFacilityAmt\":\"\",\"FourthRepaymt\":\"15\",\"FourthIntrstRate\":\"\",\"FourthMonthlyInstmt\":\"\",\"FourthTermLoanAmt\":\"\",\"FourthInterest\":\"21\",\"FourthODLoan\":\"\",\"FourthMRTA\":\"\",\"FourthBankGuarantee\":\"\",\"FourthLetterofCredit\":\"\",\"FourthTrustReceipt\":\"\",\"FourthOthers\":\"\",\"FifthTypeofFacility\":\"Sample 5\",\"FifthFacilityAmt\":\"\",\"FifthRepaymt\":\"\",\"FifthIntrstRate\":\"\",\"FifthMonthlyInstmt\":\"\",\"FifthTermLoanAmt\":\"\",\"FifthInterest\":\"10\",\"FifthODLoan\":\"\",\"FifthMRTA\":\"\",\"FifthBankGuarantee\":\"\",\"FifthLetterofCredit\":\"\",\"FifthTrustReceipt\":\"Test\",\"FifthOthers\":\"\"}}]";
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
			
		RequestParams params = new RequestParams();
		params.put("sJsonInput", list.toString());
		System.out.println("params Data Property confirm");
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
					Intent iAddBack = new Intent(ProcessCaseProperty.this, ProcessCaseLoanPrincipal.class);
					startActivity(iAddBack);
					Toast.makeText(ProcessCaseProperty.this, messageDisplay, Toast.LENGTH_SHORT).show();
				} else {
					Toast.makeText(ProcessCaseProperty.this, messageDisplay, Toast.LENGTH_SHORT).show();

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
	

public boolean dispatchTouchEvent(MotionEvent ev) {
	InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
	imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
	return super.dispatchTouchEvent(ev);

}
	
	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		if (v == details_btn) {
			 Intent to_purchaser = new Intent(ProcessCaseProperty.this, ProcessCaseDetails.class);
	           startActivity(to_purchaser);
		}
		if (v == purchaser_btn) {
	        Intent to_purchaser = new Intent(ProcessCaseProperty.this, ProcessCasePurchaser.class);
	        startActivity(to_purchaser);
	    }
	    if (v == vendor_btn) {
	        Intent to_vendor = new Intent(ProcessCaseProperty.this, ProcessCaseVendor.class);
	        startActivity(to_vendor);
	    }
	    if (v == property_btn) {
	        Intent to_loan_pricipal = new Intent(ProcessCaseProperty.this, ProcessCaseProperty.class);
	        startActivity(to_loan_pricipal);
	    }
	    if (v == loan_principal_btn) {
	        Intent to_loan_pricipal = new Intent(ProcessCaseProperty.this, ProcessCaseLoanPrincipal.class);
	        startActivity(to_loan_pricipal);
	    }
	    if (v == loan_subsidary_btn) {
	        Intent to_loan_subsidiary = new Intent(ProcessCaseProperty.this, ProcesscaseLoanSubsidiary.class);
	        startActivity(to_loan_subsidiary);
	    }
	    if (v == process_btn) {
	        Intent to_loan_subsidiary = new Intent(ProcessCaseProperty.this, ProcessCaseProcessTab.class);
	        startActivity(to_loan_subsidiary);
	    }
		if(v == title_search)
        {
			 new DatePickerDialog(ProcessCaseProperty.this, title_search1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if(v == date_submit_consent)
        {
			 new DatePickerDialog(ProcessCaseProperty.this, date_submit_consent1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if(v == date_receive_consent)
        {
			 new DatePickerDialog(ProcessCaseProperty.this, date_receive_consent1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if(v == date14_a)
        {
			 new DatePickerDialog(ProcessCaseProperty.this, date14_a1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
        if(v == redemption_date)
        {
			 new DatePickerDialog(ProcessCaseProperty.this, redemption_date1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }
        if(v == receipt_date)
        {
			 new DatePickerDialog(ProcessCaseProperty.this, receipt_date1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if(v == date_of_return)
        {
			 new DatePickerDialog(ProcessCaseProperty.this, date_of_return1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
	    if(v == walkin)
        {
        	Intent i = new Intent(ProcessCaseProperty.this, WalkInActivity.class);
			startActivity(i);
        }
	    
		if (v == confirm_btn) {
			try {
				confirm_values();
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}
	
	

	DatePickerDialog.OnDateSetListener receipt_date1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        receipt_date.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	
	DatePickerDialog.OnDateSetListener redemption_date1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        redemption_date.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener title_search1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        title_search.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener date_submit_consent1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        date_submit_consent.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener date_receive_consent1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        date_receive_consent.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener date14_a1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        date14_a.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener date_of_return1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        date_of_return.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
}
