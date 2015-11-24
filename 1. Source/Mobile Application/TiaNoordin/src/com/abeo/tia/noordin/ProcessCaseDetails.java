package com.abeo.tia.noordin;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.Toast;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

import abeo.tia.noordin.R;

/**
 * Created by Karthik on 10/7/2015.
 */
public class ProcessCaseDetails extends BaseActivity implements OnClickListener {
    private String[] navMenuTitles;
    private TypedArray navMenuIcons;
    ProgressDialog dialog = null;
    private final String COMMON_URL = "http://54.251.51.69:3878/SPAMobile.asmx";
    private final String METHOD_PROCESS_CASE_DETAILS = "SPA_ProcessCase_GetDataFromOCRD";

    Button purchaser_btn, vendor_btn, property_btn, loan_principal_btn, loan_subsidary_btn, process_btn,walkin;

    EditText file_open_date, case_fileno, case_type;
    EditText process_la, process_manager, process_incharge, process_cust_service, process_case_type, process_filelocation, process_filecloasedate, process_vend, process_company, process_bank;

    // spinner declaration
    Spinner spinner_case_status, spinner_kiv;
    TextView ID, TEXT;
    String caseValue_id = "", titleValue = "", casetype = "", casetype_value = "",CASEFILENUMBER,userName,Scase_status,Skiv;
    
    Button confirm_btn;

    // Get Project value fromapi
    private final String GET_SPINNER_VALUES = "SPA_GetValidValues";
    private final String CASE_CLOSED_CONFIRM = "SPA_ProcessCase_CloseCase";
    ArrayList<HashMap<String, String>> jsonArraylist = null;
    String id, name;
    SimpleAdapter sAdapPROJ;

    JSONArray arrayResponse = null;
    JSONObject jsonResponse = null;

    JSONArray arrOfJson = new JSONArray();

    @Override
    protected void onCreate(
            Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_process_case_details);

        // load title from strings.xml
        navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
        // load icons from strings.xml
        navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
        set(navMenuTitles, navMenuIcons);


     // Find the SharedPreferences Firstname
     			SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);		
     			String FirName = FirstName.getString("FIRSETNAME", "");
     			TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
     			welcome.setText("Welcome "+FirName);

        // Find the SharedPreferences pass Login value
        SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
        System.out.println("LOGIN DATA");
        userName = prefLoginReturn.getString("sUserName", "");

        String category = prefLoginReturn.getString("sCategory", "");
        System.out.println(category);
        String CardCode = prefLoginReturn.getString("CardCode", "");
        System.out.println(CardCode);
		sUserRole = prefLoginReturn.getString("sUserRole", "");
		Log.e("USER CREDENTIALS", sUserRole);
		System.out.println(sUserRole);
		String CaseNo = prefLoginReturn.getString("CaseNo", "");
		System.out.println(CaseNo);
		if(CaseNo=="" || CaseNo.isEmpty())
			CASEFILENUMBER = "1500000001";
		else
			CASEFILENUMBER = CaseNo;

        //buttons initialization
        purchaser_btn = (Button) findViewById(R.id.button_ProcessCase1Purchaser);
        vendor_btn = (Button) findViewById(R.id.button_ProcessCase1Vendor);
        property_btn = (Button) findViewById(R.id.button_ProcesCase1Property);
        loan_principal_btn = (Button) findViewById(R.id.button_ProcessCase1LoanPrincipal);
        loan_subsidary_btn = (Button) findViewById(R.id.button_ProcesCase1LoanSubsidiary);
        process_btn = (Button) findViewById(R.id.button_ProcessCase1Process);
		     walkin = (Button) findViewById(R.id.button_ProcessCase1Walkin);
		confirm_btn = (Button) findViewById(R.id.button_ProcessCase1Confirm);
        //spinners initialization
        spinner_case_status = (Spinner) findViewById(R.id.case_status);
        spinner_kiv = (Spinner) findViewById(R.id.spinner_ProcessCase1KIV);
        
        if(sUserRole.equals("LA")){
        	spinner_case_status.setEnabled(true);
        }else {
			spinner_case_status.setEnabled(false);
		}

        purchaser_btn.setOnClickListener(this);
        vendor_btn.setOnClickListener(this);
        property_btn.setOnClickListener(this);
        loan_subsidary_btn.setOnClickListener(this);
        loan_principal_btn.setOnClickListener(this);
        process_btn.setOnClickListener(this);
        walkin.setOnClickListener(this);
		 confirm_btn.setOnClickListener(this);

        //edittext
        case_fileno = (EditText) findViewById(R.id.editText_ProcessCase1CaseFileNo);
        case_type = (EditText) findViewById(R.id.case_type);
        file_open_date = (EditText) findViewById(R.id.editText_ProcessCase1FileOpenDate);
        process_la = (EditText) findViewById(R.id.editText_ProcessCase1LA);
        process_manager = (EditText) findViewById(R.id.editText_ProcessCase1Manger);
        process_incharge = (EditText) findViewById(R.id.editText_ProcessCase1InCharge);
        process_cust_service = (EditText) findViewById(R.id.editText_PropertyFormerlyKnownAs);
        process_case_type = (EditText) findViewById(R.id.editText_ProcessCase1InCaseType);
        process_filelocation = (EditText) findViewById(R.id.editText_ProcessCase1FileLocation);
        process_filecloasedate = (EditText) findViewById(R.id.editText_ProcessCase1FileCloseDate);
        process_vend = (EditText) findViewById(R.id.editText_ProcessCase1VendAcqDt);
        process_company = (EditText) findViewById(R.id.editText_ProcessCase1ComBussSearch);
        process_bank = (EditText) findViewById(R.id.editText_ProcessCase1BankruptcySearch);

        try {
            setRequestData();
        } catch (JSONException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }


        // Spinner click listener
        spinner_case_status.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
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
        spinner_kiv.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
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

        
    }

    public void dropdownKIV() throws JSONException {
        RequestParams params = null;
        params = new RequestParams();

        JSONObject jsonObject = new JSONObject();
        jsonObject.put("TableName", "OCRD");
        jsonObject.put("FieldName", "KIVSTATUS");
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

                    arrayResponse = new JSONArray(arg2);
                    // Create new list
                    jsonArraylist = new ArrayList<HashMap<String, String>>();

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

                        jsonArraylist.add(proList);
                        System.out.println("JSON PROPERTY LIST");
                        System.out.println(jsonArraylist);
                    }
                    // Spinner set Array Data in Drop down

                    sAdapPROJ = new SimpleAdapter(ProcessCaseDetails.this, jsonArraylist, R.layout.spinner_item,
                            new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

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


    private void setRequestData() throws JSONException {

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
		System.out.println("ThomTestddd");
		System.out.println(sCaseNo);

        final JSONObject jsonObject = new JSONObject();
        jsonObject.put("CaseNo", sCaseNo);
        jsonObject.put("UserName", user_name);
        jsonObject.put("UserRole", sUserRole);
        params.put("sJsonInput", jsonObject.toString());
        System.out.println(params);
        dialog = ProgressDialog.show(ProcessCaseDetails.this, "", "Loading Data...", true);
        RestService.post(METHOD_PROCESS_CASE_DETAILS, params, new BaseJsonHttpResponseHandler<String>() {

            @Override
            public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
                // TODO Auto-generated method stub
                System.out.println(arg3);
                System.out.println("Failedssssss");
                dialog.dismiss();

            }

            @Override
            public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
                // TODO Auto-generated method stub
                System.out.println("property Dropdown Success Details ");
                System.out.println(arg2);

                setallvalues(arg2);
                dialog.dismiss();


            }

            @Override
            protected String parseResponse(String arg0, boolean arg1) throws Throwable {

                // Get Json response
                JSONArray arrayResponse = new JSONArray(arg0);
                jsonResponse = arrayResponse.getJSONObject(0);

                System.out.println("Property Dropdown Details parse Response");
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
                case_fileno.setText(jsonobject.getString("CaseFileNo"));
                case_type.setText(jsonobject.getString("CaseType"));

                JSONObject obj1 = jsonobject.getJSONObject("Details");

                process_la.setText(obj1.getString("LA"));
                process_manager.setText(obj1.getString("MANAGER"));
                process_incharge.setText(obj1.getString("InCharge"));
                process_cust_service.setText(obj1.getString("CustomerService"));
                process_case_type.setText(obj1.getString("CaseType"));
                process_filelocation.setText(obj1.getString("FileLocation"));
                process_filecloasedate.setText(obj1.getString("FileClosedDate"));
                process_vend.setText(obj1.getString("VendAcqDt"));
                process_company.setText(obj1.getString("CompanyBuisnessSearch"));
                process_bank.setText(obj1.getString("BankWindingSearch"));
                
                Scase_status = jsonobject.getString("CaseStatus");
                Skiv = jsonobject.getString("KIV");
                
               
                
                dropdownstatus();
                dropdownKIV();
                
                SharedPreferences prefLogin = getSharedPreferences("KVIData", Context.MODE_PRIVATE);

				// We need an editor object to make changes
				SharedPreferences.Editor edit = prefLogin.edit();

				// Set/Store data
				edit.putString("CaseStatus", Scase_status);
				edit.putString("KIV", Skiv);
				// Commit the changes
				edit.commit();
                
                
            }
            System.out.println(jObj);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void dropdownstatus() throws JSONException {
        RequestParams params = null;
        params = new RequestParams();

        JSONObject jsonObject = new JSONObject();
        jsonObject.put("TableName", "OCRD");
        jsonObject.put("FieldName", "CASESTATUS");
        params.put("sJsonInput", jsonObject.toString());
        System.out.println(params);

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

                    arrayResponse = new JSONArray(arg2);
                    // Create new list
                    jsonArraylist = new ArrayList<HashMap<String, String>>();

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

                        jsonArraylist.add(proList);
                        System.out.println("JSON PROPERTY LIST");
                        System.out.println(jsonArraylist);
                    }
                    // Spinner set Array Data in Drop down

                    sAdapPROJ = new SimpleAdapter(ProcessCaseDetails.this, jsonArraylist, R.layout.spinner_item,
                            new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

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

    public String getSelectedItem(String a)
	{
		String[] separated = a.split(",");
		String[] finaltext = separated[0].split("="); 
		return finaltext[1];
		
	}
    private void confirmDetails() {
  		// TODO Auto-generated method stub
    	try {
    		dialog = ProgressDialog.show(ProcessCaseDetails.this, "", "Sending Data...", true);
			caseClosed();
			
        } catch (JSONException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    	
    
  	}
    
    
    private void caseClosed() throws JSONException{
		// TODO Auto-generated method stub
    	RequestParams params = null;
        params = new RequestParams();
        String status_spinner = getSelectedItem(((Spinner)findViewById(R.id.case_status)).getSelectedItem().toString());
        String kiv_spinner = getSelectedItem(((Spinner)findViewById(R.id.spinner_ProcessCase1KIV)).getSelectedItem().toString());
        JSONObject jsonObject = new JSONObject();
        jsonObject.put("sCaseNo", CASEFILENUMBER);
        jsonObject.put("sUserName", userName);
        jsonObject.put("sStatus", status_spinner);
        jsonObject.put("sKIV", kiv_spinner);
        params.put("sJsonInput", jsonObject.toString());
        RestService.post(CASE_CLOSED_CONFIRM, params, new BaseJsonHttpResponseHandler<String>() {

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
				System.out.println("GetOpitanal Success Details ");
				System.out.println(arg2);
				String messageDisplay="",Result="";
				
					try {
						arrayResponse = new JSONArray(arg2);
					
				
				
					jsonResponse = arrayResponse.getJSONObject(0);
					
					 messageDisplay = jsonResponse.getString("DisplayMessage").toString();
					 Result = jsonResponse.getString("Result").toString();
					
							
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					if(Result.equals("CLOSED"))
					{
						Toast.makeText(ProcessCaseDetails.this, messageDisplay, Toast.LENGTH_LONG).show();
						 Intent to_dash = new Intent(ProcessCaseDetails.this,  DashBoardActivity.class);
				         startActivity(to_dash);
					}
					else
					{
						Toast.makeText(ProcessCaseDetails.this, messageDisplay, Toast.LENGTH_LONG).show();
					}
					
					dialog.dismiss();
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
}

	@Override
    public void onClick(View v) {
    	
    	//purchaser_btn.setOnClickListener(this);
       // vendor_btn.setOnClickListener(this);
       // property_btn.setOnClickListener(this);
        //loan_subsidary_btn.setOnClickListener(this);
       // loan_principal_btn.setOnClickListener(this);
        //process_btn.setOnClickListener(this);
        
        
        if (v == purchaser_btn) {
            Intent to_purchaser = new Intent(ProcessCaseDetails.this, ProcessCasePurchaser.class);
            startActivity(to_purchaser);
        }
        if (v == vendor_btn) {
            Intent to_vendor = new Intent(ProcessCaseDetails.this, ProcessCaseVendor.class);
            startActivity(to_vendor);
        }
        if (v == property_btn) {
            Intent to_loan_pricipal = new Intent(ProcessCaseDetails.this, ProcessCaseProperty.class);
            startActivity(to_loan_pricipal);
        }
        if (v == loan_principal_btn) {
            Intent to_loan_pricipal = new Intent(ProcessCaseDetails.this, ProcessCaseLoanPrincipal.class);
            startActivity(to_loan_pricipal);
        }
        if (v == loan_subsidary_btn) {
            Intent to_loan_subsidiary = new Intent(ProcessCaseDetails.this, ProcesscaseLoanSubsidiary.class);
            startActivity(to_loan_subsidiary);
        }
        if (v == process_btn) {
            Intent to_loan_subsidiary = new Intent(ProcessCaseDetails.this, ProcessCaseProcessTab.class);
            startActivity(to_loan_subsidiary);
        }
        if(v == walkin)
        {
        	Intent i = new Intent(ProcessCaseDetails.this, WalkInActivity.class);
			startActivity(i);
        }
		if (v == confirm_btn) {
			confirmDetails();
		}
    }
    public boolean dispatchTouchEvent(MotionEvent ev) {
		InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
		imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
		return super.dispatchTouchEvent(ev);

	}

}