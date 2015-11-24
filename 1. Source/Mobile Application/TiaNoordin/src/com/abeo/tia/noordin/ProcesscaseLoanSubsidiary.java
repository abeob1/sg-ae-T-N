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

import android.app.DatePickerDialog;
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
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;

import abeo.tia.noordin.R;

/**
 * Created by Karthik on 10/9/2015.
 */
public class ProcesscaseLoanSubsidiary extends BaseActivity implements
		OnClickListener {
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	
	//datepicker
		String myFormat = "MM/dd/yyyy";
		Calendar myCalendar = Calendar.getInstance();

    private final String COMMON_URL = "http://54.251.51.69:3878/SPAMobile.asmx";
    private final String METHOD_PROCESS_CASE_DETAILS = "SPA_ProcessCase_GetDataFromOCRD";
    private final String CONFIRM_BTN_REQUEST = "SPA_ProcessCase_UpdateCaseTabDetails";
    
    String messageDisplay = "", StatusResult = "",f1Value,f1_id,f2Value,f2_id,f3Value,f3_id,f4Value,f4_id,f5Value,f5_id,Ssf1,Ssf2,Ssf3,Ssf4,Ssf5,Scase_status,Skiv;
    
    

    Button purchaser_btn, vendor_btn, property_btn, loan_principal_btn, loan_subsidary_btn, process_btn,confirm_allvalues_btn,details_btn,walkin;

    EditText file_open_date, case_fileno, case_type,
    ELoanDocForBankExe,
    EFaciAgreeDate,
    EeditText_loan_doc_bank,
    EeditText_dischargeofcharge,
    EeditText_type_facility,
    EeditText_fac_amount,
    EeditText_repayment,
    EeditText_interestrate,
    EeditText_monthly_installment,
    EeditText_loan_amt,
    EeditText_interest,
    EeditText_od_loan,
    EeditText_mrta,
    EeditText_bank_guarantee,
    EeditText_letterofcredit,
    EeditText_trust_receipt,
    EeditText_others,

    EeditText_type_facility1,
    EeditText_fac_amount1,
    EeditText_repayment1,
    EeditText_interestrate1,
    EeditText_monthly_installment1,
    EeditText_loan_amt1,
    EeditText_interest1,
    EeditText_od_loan1,
    EeditText_mrta1,
    EeditText_bank_guarantee1,
    EeditText_letterofcredit1,
    EeditText_trust_receipt1,
    EeditText_others1,
    
    EeditText_type_facility2,
    EeditText_fac_amount2,
    EeditText_repayment2,
    EeditText_interestrate2,
    EeditText_monthly_installment2,
    EeditText_loan_amt2,
    EeditText_interest2,
    EeditText_od_loan2,
    EeditText_mrta2,
    EeditText_bank_guarantee2,
    EeditText_letterofcredit2,
    EeditText_trust_receipt2,
    EeditText_others2,
    
    EeditText_type_facility3,
    EeditText_fac_amount3,
    EeditText_repayment3,
    EeditText_interestrate3,
    EeditText_monthly_installment3,
    EeditText_loan_amt3,
    EeditText_interest3,
    EeditText_od_loan3,
    EeditText_mrta3,
    EeditText_bank_guarantee3,
    EeditText_letterofcredit3,
    EeditText_trust_receipt3,
    EeditText_others3,
    
    EeditText_type_facility4,
    EeditText_fac_amount4,
    EeditText_repayment4,
    EeditText_interestrate4,
    EeditText_monthly_installment4,
    EeditText_loan_amt4,
    EeditText_interest4,
    EeditText_od_loan4,
    EeditText_mrta4,
    EeditText_bank_guarantee4,
    EeditText_letterofcredit4,
    EeditText_trust_receipt4,
    EeditText_others4;
    

    // spinner declaration
    Spinner spinner_case_status, spinner_kiv,sf1,sf2,sf3,sf4,sf5;
    TextView ID, TEXT;
    String caseValue_id = "", titleValue = "", casetype = "", casetype_value = "";

    // Get Project value fromapi
    private final String GET_SPINNER_VALUES = "SPA_GetValidValues";
    ArrayList<HashMap<String, String>> jsonArraylist = null;
    String id, name;
    SimpleAdapter sAdapPROJ;

    JSONArray arrayResponse = null;
    JSONObject jsonResponse = null,jsonResponseconfirm=null;

    JSONArray arrOfJson = new JSONArray();

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_process_case_loansubsidiary);

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
		
		//buttons initialization
		details_btn = (Button) findViewById(R.id.button_ProcessCase1Details);
        purchaser_btn = (Button) findViewById(R.id.button_ProcessCase1Purchaser);
        vendor_btn = (Button) findViewById(R.id.button_ProcessCase1Vendor);
        property_btn = (Button) findViewById(R.id.button_ProcesCase1Property);
        loan_principal_btn = (Button) findViewById(R.id.button_ProcessCase1LoanPrincipal);
        loan_subsidary_btn = (Button) findViewById(R.id.button_ProcesCase1LoanSubsidiary);
        process_btn = (Button) findViewById(R.id.button_ProcessCase1Process);
        confirm_allvalues_btn = (Button) findViewById(R.id.button_ProcessCase1Confirm);
        walkin = (Button) findViewById(R.id.button_ProcessCase1Walkin);
        
        
        
        //all edite field
        file_open_date=(EditText) findViewById(R.id.	editText_ProcessCase1FileOpenDate	);
        case_fileno=(EditText) findViewById(R.id.	editText_ProcessCase1CaseFileNo	);
        case_type=(EditText) findViewById(R.id.	editText_LoanCaseType);
        
        
        ELoanDocForBankExe=	(EditText) findViewById(R.id.	LoanDocForBankExe	);
        EFaciAgreeDate=	(EditText) findViewById(R.id.	FaciAgreeDate	);
        EeditText_loan_doc_bank=	(EditText) findViewById(R.id.	editText_loan_doc_bank	);
        EeditText_dischargeofcharge=	(EditText) findViewById(R.id.	editText_dischargeofcharge	);
       // EeditText_type_facility=	(EditText) findViewById(R.id.	editText_type_facility	);
        EeditText_fac_amount=	(EditText) findViewById(R.id.	editText_fac_amount	);
        EeditText_repayment=	(EditText) findViewById(R.id.	editText_repayment	);
        EeditText_interestrate=	(EditText) findViewById(R.id.	editText_interestrate	);
        EeditText_monthly_installment=	(EditText) findViewById(R.id.	editText_monthly_installment	);
        EeditText_loan_amt=	(EditText) findViewById(R.id.	editText_loan_amt	);
        EeditText_interest=	(EditText) findViewById(R.id.	editText_interest	);
        EeditText_od_loan=	(EditText) findViewById(R.id.	editText_od_loan	);
        EeditText_mrta=	(EditText) findViewById(R.id.	editText_mrta	);
        EeditText_bank_guarantee=	(EditText) findViewById(R.id.	editText_bank_guarantee	);
        EeditText_letterofcredit=	(EditText) findViewById(R.id.	editText_letterofcredit	);
        EeditText_trust_receipt=	(EditText) findViewById(R.id.	editText_trust_receipt	);
        EeditText_others=	(EditText) findViewById(R.id.	editText_others	);
      //  EeditText_type_facility1=	(EditText) findViewById(R.id.	editText_type_facility1	);
        EeditText_fac_amount1=	(EditText) findViewById(R.id.	editText_fac_amount1	);
        EeditText_repayment1=	(EditText) findViewById(R.id.	editText_repayment1	);
        EeditText_interestrate1=	(EditText) findViewById(R.id.	editText_interestrate1	);
        EeditText_monthly_installment1=	(EditText) findViewById(R.id.	editText_monthly_installment1	);
        EeditText_loan_amt1=	(EditText) findViewById(R.id.	editText_loan_amt1	);
        EeditText_interest1=	(EditText) findViewById(R.id.	editText_interest1	);
        EeditText_od_loan1=	(EditText) findViewById(R.id.	editText_od_loan1	);
        EeditText_mrta1=	(EditText) findViewById(R.id.	editText_mrta1	);
        EeditText_bank_guarantee1=	(EditText) findViewById(R.id.	editText_bank_guarantee1	);
        EeditText_letterofcredit1=	(EditText) findViewById(R.id.	editText_letterofcredit1	);
        EeditText_trust_receipt1=	(EditText) findViewById(R.id.	editText_trust_receipt1	);
        EeditText_others1=	(EditText) findViewById(R.id.	editText_others1	);
       // EeditText_type_facility2=	(EditText) findViewById(R.id.	editText_type_facility2	);
        EeditText_fac_amount2=	(EditText) findViewById(R.id.	editText_fac_amount2	);
        EeditText_repayment2=	(EditText) findViewById(R.id.	editText_repayment2	);
        EeditText_interestrate2=	(EditText) findViewById(R.id.	editText_interestrate2	);
        EeditText_monthly_installment2=	(EditText) findViewById(R.id.	editText_monthly_installment2	);
        EeditText_loan_amt2=	(EditText) findViewById(R.id.	editText_loan_amt2	);
        EeditText_interest2=	(EditText) findViewById(R.id.	editText_interest2	);
        EeditText_od_loan2=	(EditText) findViewById(R.id.	editText_od_loan2	);
        EeditText_mrta2=	(EditText) findViewById(R.id.	editText_mrta2	);
        EeditText_bank_guarantee2=	(EditText) findViewById(R.id.	editText_bank_guarantee2	);
        EeditText_letterofcredit2=	(EditText) findViewById(R.id.	editText_letterofcredit2	);
        EeditText_trust_receipt2=	(EditText) findViewById(R.id.	editText_trust_receipt2	);
        EeditText_others2=	(EditText) findViewById(R.id.	editText_others2	);
      //  EeditText_type_facility3=	(EditText) findViewById(R.id.	editText_type_facility3	);
        EeditText_fac_amount3=	(EditText) findViewById(R.id.	editText_fac_amount3	);
        EeditText_repayment3=	(EditText) findViewById(R.id.	editText_repayment3	);
        EeditText_interestrate3=	(EditText) findViewById(R.id.	editText_interestrate3	);
        EeditText_monthly_installment3=	(EditText) findViewById(R.id.	editText_monthly_installment3	);
        EeditText_loan_amt3=	(EditText) findViewById(R.id.	editText_loan_amt3	);
        EeditText_interest3=	(EditText) findViewById(R.id.	editText_interest3	);
        EeditText_od_loan3=	(EditText) findViewById(R.id.	editText_od_loan3	);
        EeditText_mrta3=	(EditText) findViewById(R.id.	editText_mrta3	);
        EeditText_bank_guarantee3=	(EditText) findViewById(R.id.	editText_bank_guarantee3	);
        EeditText_letterofcredit3=	(EditText) findViewById(R.id.	editText_letterofcredit3	);
        EeditText_trust_receipt3=	(EditText) findViewById(R.id.	editText_trust_receipt3	);
        EeditText_others3=	(EditText) findViewById(R.id.	editText_others3	);
      //  EeditText_type_facility4=	(EditText) findViewById(R.id.	editText_type_facility4	);
        EeditText_fac_amount4=	(EditText) findViewById(R.id.	editText_fac_amount4	);
        EeditText_repayment4=	(EditText) findViewById(R.id.	editText_repayment4	);
        EeditText_interestrate4=	(EditText) findViewById(R.id.	editText_interestrate4	);
        EeditText_monthly_installment4=	(EditText) findViewById(R.id.	editText_monthly_installment4	);
        EeditText_loan_amt4=	(EditText) findViewById(R.id.	editText_loan_amt4	);
        EeditText_interest4=	(EditText) findViewById(R.id.	editText_interest4	);
        EeditText_od_loan4=	(EditText) findViewById(R.id.	editText_od_loan4	);
        EeditText_mrta4=	(EditText) findViewById(R.id.	editText_mrta4	);
        EeditText_bank_guarantee4=	(EditText) findViewById(R.id.	editText_bank_guarantee4	);
        EeditText_letterofcredit4=	(EditText) findViewById(R.id.	editText_letterofcredit4	);
        EeditText_trust_receipt4=	(EditText) findViewById(R.id.	editText_trust_receipt4	);
        EeditText_others4=	(EditText) findViewById(R.id.	editText_others4	);

        

        //spinners initialization
        spinner_case_status = (Spinner) findViewById(R.id.case_status);
        spinner_kiv = (Spinner) findViewById(R.id.spinner_ProcessCase1KIV);
        sf1 = (Spinner) findViewById(R.id.f1);
        sf2 = (Spinner) findViewById(R.id.f2);
        sf3 = (Spinner) findViewById(R.id.f3);
        sf4 = (Spinner) findViewById(R.id.f4);
        sf5 = (Spinner) findViewById(R.id.f5);

        details_btn.setOnClickListener(this);
        purchaser_btn.setOnClickListener(this);
        vendor_btn.setOnClickListener(this);
        property_btn.setOnClickListener(this);
        loan_subsidary_btn.setOnClickListener(this);
        loan_principal_btn.setOnClickListener(this);
        process_btn.setOnClickListener(this);
        confirm_allvalues_btn.setOnClickListener(this);
        walkin.setOnClickListener(this);
        
        ELoanDocForBankExe.setOnClickListener(this);
        EFaciAgreeDate.setOnClickListener(this);
        EeditText_loan_doc_bank.setOnClickListener(this);
        EeditText_dischargeofcharge.setOnClickListener(this);
	
        
     // Spinner click listener
        sf1.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                ID = (TextView) view.findViewById(R.id.Id);
                f1_id = ID.getText().toString();
                TEXT = (TextView) view.findViewById(R.id.Name);
                f1Value = TEXT.getText().toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                // TODO Auto-generated method stub

            }
        });
        
        // Spinner click listener
        sf2.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                ID = (TextView) view.findViewById(R.id.Id);
                f2_id = ID.getText().toString();
                TEXT = (TextView) view.findViewById(R.id.Name);
                f2Value = TEXT.getText().toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                // TODO Auto-generated method stub

            }
        });
        
        // Spinner click listener
        sf3.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                ID = (TextView) view.findViewById(R.id.Id);
                f3_id = ID.getText().toString();
                TEXT = (TextView) view.findViewById(R.id.Name);
                f3Value = TEXT.getText().toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                // TODO Auto-generated method stub

            }
        });
     // Spinner click listener
        sf4.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                ID = (TextView) view.findViewById(R.id.Id);
                f4_id = ID.getText().toString();
                TEXT = (TextView) view.findViewById(R.id.Name);
                f4Value = TEXT.getText().toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                // TODO Auto-generated method stub

            }
        });
        
     // Spinner click listener
        sf4.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                ID = (TextView) view.findViewById(R.id.Id);
                f4_id = ID.getText().toString();
                TEXT = (TextView) view.findViewById(R.id.Name);
                f4Value = TEXT.getText().toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                // TODO Auto-generated method stub

            }
        });
     // Spinner click listener
        sf5.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                ID = (TextView) view.findViewById(R.id.Id);
                f5_id = ID.getText().toString();
                TEXT = (TextView) view.findViewById(R.id.Name);
                f5Value = TEXT.getText().toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                // TODO Auto-generated method stub

            }
        });
        


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

    try {
       
        setRequestData();
        
        //dropdownf2();
        //dropdownf3();
        //dropdownf4();
        //dropdownf5();
    } catch (JSONException e) {
        e.printStackTrace();
    }
    
    disableHeaderfields();
}

//dropdownload

public void dropdownf1() throws JSONException {
    RequestParams params = null;
    params = new RequestParams();

    JSONObject jsonObject = new JSONObject();
    jsonObject.put("TableName", "OCRD");
    jsonObject.put("FieldName", "LOAN_PRNCP_TYPE");
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

                sAdapPROJ = new SimpleAdapter(ProcesscaseLoanSubsidiary.this, jsonArraylist, R.layout.spinner_item,
                        new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

                sf1.setAdapter(sAdapPROJ);
                sf2.setAdapter(sAdapPROJ);
                sf3.setAdapter(sAdapPROJ);
                sf4.setAdapter(sAdapPROJ);
                sf5.setAdapter(sAdapPROJ);

				for (int j = 0; j < jsonArraylist.size(); j++) {
                    if (jsonArraylist.get(j).get("Id_T").equals(Ssf1)) {
                    	sf1.setSelection(j);
						break;
					}
				}
				for (int j = 0; j < jsonArraylist.size(); j++) {
                    if (jsonArraylist.get(j).get("Id_T").equals(Ssf2)) {
                    	sf2.setSelection(j);
						break;
					}
				}
				for (int j = 0; j < jsonArraylist.size(); j++) {
                    if (jsonArraylist.get(j).get("Id_T").equals(Ssf3)) {
                    	sf3.setSelection(j);
						break;
					}
				}
				for (int j = 0; j < jsonArraylist.size(); j++) {
                    if (jsonArraylist.get(j).get("Id_T").equals(Ssf4)) {
                    	sf4.setSelection(j);
						break;
					}
				}
				for (int j = 0; j < jsonArraylist.size(); j++) {
                    if (jsonArraylist.get(j).get("Id_T").equals(Ssf5)) {
                    	sf5.setSelection(j);
						break;
					}
				}

            } catch (JSONException e) {

                e.printStackTrace();
            }
        }
    });
}

public void dropdownf2() throws JSONException {
    RequestParams params = null;
    params = new RequestParams();

    JSONObject jsonObject = new JSONObject();
    jsonObject.put("TableName", "OCRD");
    jsonObject.put("FieldName", "LOAN_PRNCP_TYPE");
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

                sAdapPROJ = new SimpleAdapter(ProcesscaseLoanSubsidiary.this, jsonArraylist, R.layout.spinner_item,
                        new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

                sf2.setAdapter(sAdapPROJ);

				/*for (int j = 0; j < jsonlistProject.size(); j++) {
                    if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
						TitleType_DROPDOWN.setSelection(j);
						break;
					}
				}*/

            } catch (JSONException e) {

                e.printStackTrace();
            }
        }
    });
}

public void dropdownf3() throws JSONException {
    RequestParams params = null;
    params = new RequestParams();

    JSONObject jsonObject = new JSONObject();
    jsonObject.put("TableName", "OCRD");
    jsonObject.put("FieldName", "LOAN_PRNCP_TYPE");
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

                sAdapPROJ = new SimpleAdapter(ProcesscaseLoanSubsidiary.this, jsonArraylist, R.layout.spinner_item,
                        new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

                sf3.setAdapter(sAdapPROJ);

				/*for (int j = 0; j < jsonlistProject.size(); j++) {
                    if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
						TitleType_DROPDOWN.setSelection(j);
						break;
					}
				}*/

            } catch (JSONException e) {

                e.printStackTrace();
            }
        }
    });
}

public void dropdownf4() throws JSONException {
    RequestParams params = null;
    params = new RequestParams();

    JSONObject jsonObject = new JSONObject();
    jsonObject.put("TableName", "OCRD");
    jsonObject.put("FieldName", "LOAN_PRNCP_TYPE");
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

                sAdapPROJ = new SimpleAdapter(ProcesscaseLoanSubsidiary.this, jsonArraylist, R.layout.spinner_item,
                        new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

                sf4.setAdapter(sAdapPROJ);

				/*for (int j = 0; j < jsonlistProject.size(); j++) {
                    if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
						TitleType_DROPDOWN.setSelection(j);
						break;
					}
				}*/

            } catch (JSONException e) {

                e.printStackTrace();
            }
        }
    });
}

public void dropdownf5() throws JSONException {
    RequestParams params = null;
    params = new RequestParams();

    JSONObject jsonObject = new JSONObject();
    jsonObject.put("TableName", "OCRD");
    jsonObject.put("FieldName", "LOAN_PRNCP_TYPE");
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

                sAdapPROJ = new SimpleAdapter(ProcesscaseLoanSubsidiary.this, jsonArraylist, R.layout.spinner_item,
                        new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

                sf5.setAdapter(sAdapPROJ);

				/*for (int j = 0; j < jsonlistProject.size(); j++) {
                    if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
						TitleType_DROPDOWN.setSelection(j);
						break;
					}
				}*/

            } catch (JSONException e) {

                e.printStackTrace();
            }
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

                sAdapPROJ = new SimpleAdapter(ProcesscaseLoanSubsidiary.this, jsonArraylist, R.layout.spinner_item,
                        new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

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
	System.out.println(sCaseNo);

    final JSONObject jsonObject = new JSONObject();
    jsonObject.put("CaseNo", sCaseNo);
    jsonObject.put("UserName", user_name);
    jsonObject.put("UserRole", sUserRole);
    params.put("sJsonInput", jsonObject.toString());

    RestService.post(METHOD_PROCESS_CASE_DETAILS, params, new BaseJsonHttpResponseHandler<String>() {

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
				JSONArray arrayRes = new JSONArray(arg2);
				jsonResponseconfirm =arrayRes.getJSONObject(0);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
            setallvalues(arg2);


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

private void confirm_allvalues() throws JSONException {
	// TODO Auto-generated method stub
	
	/*String spinnertype1 = ((Spinner)findViewById(R.id.spinner_PurchaserType)).getSelectedItem().toString();
	String spinnertype2 = ((Spinner)findViewById(R.id.spinner_PurchaserType2)).getSelectedItem().toString();
	String spinnertype3 = ((Spinner)findViewById(R.id.spinner_PurchaserType3)).getSelectedItem().toString();
	String spinnertype4 = ((Spinner)findViewById(R.id.spinner_PurchaserType4)).getSelectedItem().toString();*/
	
	
			
//	String json_element = "[{\"Case\":\"1500000001\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"CaseStatus\":\"OPEN\",\"FileOpenDate\":\"10/10/2015 12:00:00 AM\",\"CaseFileNo\":\"JJ/1500000001/\",\"KIV\":\"\",\"TabId\":\"2\",\"Details\":{\"LA\":\"\",\"MANAGER\":\"\",\"InCharge\":\"\",\"CustomerService\":\"\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"FileLocation\":\"\",\"FileClosedDate\":\"\",\"VendAcqDt\":\"\",\"CompanyBuisnessSearch\":\"\",\"BankWindingSearch\":\"\"},\"Purchaser\":{\"PurRepresentedByFirm\":\"N\",\"PurlawyerRepresented\":\"N\",\"PurSPADate\":\"1/1/1900 12:00:00 AM\",\"PurEntryOfPrivateCaveat\":\"Chase\",\"PurWithOfPrivateCaveat\":\"Rane\",\"PurFirstName\":\"Name1\",\"PurFirstID\":\"Id1\",\"PurFirstTaxNo\":\"Tax1\",\"PurFirstContactNo\":\"9784561233\",\"PurFirstType\":\"CORPORATE\",\"PurSecName\":\"Name2\",\"PurSecID\":\"Id2\",\"PurSecTaxNo\":\"Tax2\",\"PurSecContactNo\":\"9784561234\",\"PurSecType\":\"INDIVIDUAL\",\"PurThirdName\":\"Name3\",\"PurThirdID\":\"Id3\",\"PurThirdTaxNo\":\"Tax3\",\"PurThirdContactNo\":\"9784561234\",\"PurThirdType\":\"INDIVIDUAL\",\"PurFourthName\":\"Name4\",\"PurFourthID\":\"Id4\",\"PurFourthTaxNo\":\"Tax4\",\"PurFourthContactNo\":\"9784561235\",\"PurFourthType\":\"INDIVIDUAL\"},\"Vendor\":{\"VndrRepresentedByFirm\":\"N\",\"VndrlawyerRepresented\":\"Y\",\"VndrReqDevConsent\":\"Name\",\"VndrReceiveDevConsent\":\"Sam\",\"VndrFirstName\":\"Name1\",\"VndrFirstID\":\"Id1\",\"VndrFirstTaxNo\":\"Tax1\",\"VndrFirstContactNo\":\"9784561233\",\"VndrFirstType\":\"CORPORATE\",\"VndrSecName\":\"Name2\",\"VndrSecID\":\"Id2\",\"VndrSecTaxNo\":\"Tax2\",\"VndrSecContactNo\":\"9784561234\",\"VndrSecType\":\"INDIVIDUAL\",\"VndrThirdName\":\"Name3\",\"VndrThirdID\":\"Id3\",\"VndrThirdTaxNo\":\"Tax3\",\"VndrThirdContactNo\":\"9784561234\",\"VndrThirdType\":\"INDIVIDUAL\",\"VndrFourthName\":\"Name4\",\"VndrFourthID\":\"Id4\",\"VndrFourthTaxNo\":\"Tax4\",\"VndrFourthContactNo\":\"9784561235\",\"VndrFourthType\":\"INDIVIDUAL\"},\"Property\":{\"TitleType\":\"KEKAL\",\"CertifiedPlanNo\":\"\",\"LotNo\":\"\",\"PreviouslyKnownAs\":\"\",\"State\":\"\",\"Area\":\"\",\"BPM\":\"\",\"GovSurvyPlan\":\"\",\"LotArea\":\"12345\",\"Developer\":\"\",\"Project\":\"\",\"DevLicenseNo\":\"\",\"DevSolicitor\":\"\",\"DevSoliLoc\":\"\",\"TitleSearchDate\":\"\",\"DSCTransfer\":\"\",\"DRCTransfer\":\"\",\"FourteenADate\":\"\",\"DRTLRegistry\":\"\",\"PropertyCharged\":\"\",\"BankName\":\"\",\"Branch\":\"\",\"PAName\":\"\",\"PresentationNo\":\"\",\"ExistChargeRef\":\"\",\"ReceiptType\":\"\",\"ReceiptNo\":\"\",\"ReceiptDate\":\"\",\"PurchasePrice\":\"1452\",\"AdjValue\":\"\",\"VndrPrevSPAValue\":\"\",\"Deposit\":\"\",\"BalPurPrice\":\"\",\"LoanAmount\":\"\",\"LoanCaseNo\":\"1234\",\"DiffSum\":\"\",\"RedAmt\":\"\",\"RedDate\":\"\",\"DefRdmptSum\":\"\"},\"LoanPrinciple\":{\"ReqRedStatement\":\"\",\"RedStmtDate\":\"\",\"RedPayDate\":\"\",\"RepByFirm\":\"\",\"LoanCaseNo\":\"1234\",\"Project\":\"\",\"MasterBankName\":\"\",\"BranchName\":\"\",\"Address\":\"\",\"PAName\":\"\",\"BankRef\":\"\",\"BankInsDate\":\"2015/10/11\",\"LOFDate\":\"\",\"BankSolicitor\":\"\",\"SoliLoc\":\"\",\"SoliRef\":\"\",\"TypeofLoan\":\"\",\"TypeofFacility\":\"\",\"FacilityAmt\":\"\",\"Repaymt\":\"\",\"IntrstRate\":\"\",\"MonthlyInstmt\":\"\",\"TermLoanAmt\":\"\",\"Interest\":\"\",\"ODLoan\":\"\",\"MRTA\":\"\",\"BankGuarantee\":\"\",\"LetterofCredit\":\"\",\"TrustReceipt\":\"\",\"Others\":\"\",\"LoanDet1\":\"\",\"LoanDet2\":\"\",\"LoanDet3\":\"\",\"LoanDet4\":\"\",\"LoanDet5\":\"\"},\"LoanSubsidary\":{\"LoanDocForBankExe\":\"Exe 1\",\"FaciAgreeDate\":\"\",\"LoanDocRetFromBank\":\"\",\"DischargeofCharge\":\"\",\"FirstTypeofFacility\":\"\",\"FirstFacilityAmt\":\"\",\"FirstRepaymt\":\"\",\"FirstIntrstRate\":\"\",\"FirstMonthlyInstmt\":\"1500\",\"FirstTermLoanAmt\":\"\",\"FirstInterest\":\"\",\"FirstODLoan\":\"\",\"FirstMRTA\":\"\",\"FirstBankGuarantee\":\"\",\"FirstLetterofCredit\":\"\",\"FirstTrustReceipt\":\"\",\"FirstOthers\":\"Sample\",\"SecTypeofFacility\":\"\",\"SecFacilityAmt\":\"\",\"SecRepaymt\":\"\",\"SecIntrstRate\":\"12%\",\"SecMonthlyInstmt\":\"\",\"SecTermLoanAmt\":\"\",\"SecInterest\":\"\",\"SecODLoan\":\"\",\"SecMRTA\":\"\",\"SecBankGuarantee\":\"\",\"SecLetterofCredit\":\"\",\"SecTrustReceipt\":\"45A\",\"SecOthers\":\"\",\"ThirdTypeofFacility\":\"Sample_Data\",\"ThirdFacilityAmt\":\"\",\"ThirdRepaymt\":\"\",\"ThirdIntrstRate\":\"\",\"ThirdMonthlyInstmt\":\"\",\"ThirdTermLoanAmt\":\"587.15\",\"ThirdInterest\":\"\",\"ThirdODLoan\":\"\",\"ThirdMRTA\":\"\",\"ThirdBankGuarantee\":\"\",\"ThirdLetterofCredit\":\"\",\"ThirdTrustReceipt\":\"\",\"ThirdOthers\":\"\",\"FourthTypeofFacility\":\"Sample4\",\"FourthFacilityAmt\":\"\",\"FourthRepaymt\":\"15\",\"FourthIntrstRate\":\"\",\"FourthMonthlyInstmt\":\"\",\"FourthTermLoanAmt\":\"\",\"FourthInterest\":\"21\",\"FourthODLoan\":\"\",\"FourthMRTA\":\"\",\"FourthBankGuarantee\":\"\",\"FourthLetterofCredit\":\"\",\"FourthTrustReceipt\":\"\",\"FourthOthers\":\"\",\"FifthTypeofFacility\":\"Sample 5\",\"FifthFacilityAmt\":\"\",\"FifthRepaymt\":\"\",\"FifthIntrstRate\":\"\",\"FifthMonthlyInstmt\":\"\",\"FifthTermLoanAmt\":\"\",\"FifthInterest\":\"10\",\"FifthODLoan\":\"\",\"FifthMRTA\":\"\",\"FifthBankGuarantee\":\"\",\"FifthLetterofCredit\":\"\",\"FifthTrustReceipt\":\"Test\",\"FifthOthers\":\"\"}}]";
	//String json_element = "[{\"Case\":\"1500000001\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"CaseStatus\":\"OPEN\",\"FileOpenDate\":\"10/10/2015 12:00:00 AM\",\"CaseFileNo\":\"JJ/1500000001/\",\"KIV\":\"\",\"TabId\":\"2\",\"Details\":{\"LA\":\"\",\"MANAGER\":\"\",\"InCharge\":\"\",\"CustomerService\":\"\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"FileLocation\":\"\",\"FileClosedDate\":\"\",\"VendAcqDt\":\"\",\"CompanyBuisnessSearch\":\"\",\"BankWindingSearch\":\"\"},\"Purchaser\":{\"" + "PurRepresentedByFirm" + "\":" + "\"" + f_yes + "\",\"" + "PurlawyerRepresented" + "\":" + "\"" + l_yes + "\",\"" + "PurSPADate" + "\":" + "\"" + spa_date.getText().toString() + "\", \"" + "PurEntryOfPrivateCaveat" + "\":" + "\"" + entry_private_caveat.getText().toString() + "\", \"" + "PurWithOfPrivateCaveat" + "\":" + "\"" + withdrawal_private_caveat.getText().toString() + "\" ,\"" + "PurFirstName" + "\":" + "\"" + name1.getText().toString() + "\",\"" + "PurFirstID" + "\":" + "\"" + brn_no1.getText().toString() + "\",\"" + "PurFirstTaxNo" + "\":" + "\"" + tax_no1.getText().toString() + "\",\"" + "PurFirstContactNo" + "\":" + "\"" + contact_no1.getText().toString() + "\",\"" + "PurFirstType" + "\":" + "\"" + spinnertype1 + "\",\"" + "PurSecName" + "\":" + "\"" + name2.getText().toString() + "\",\"" + "PurSecID" + "\":" + "\"" + brn_no2.getText().toString() + "\",\"" + "PurSecTaxNo" + "\":" + "\"" + tax_no2.getText().toString() + "\",\"" + "PurSecContactNo" + "\":" + "\"" + contact_no2.getText().toString() + "\",\"" + "PurSecType" + "\":" + "\"" + spinnertype2 + "\",\"" + "PurThirdName" + "\":" + "\"" + name3.getText().toString() + "\",\"" + "PurThirdID" + "\":" + "\"" + brn_no3.getText().toString() + "\",\"" + "PurThirdTaxNo" + "\":" + "\"" + tax_no3.getText().toString() + "\",\"" + "PurThirdContactNo" + "\":" + "\"" + contact_no3.getText().toString() + "\",\"" + "PurThirdType" + "\":" + "\"" + spinnertype3 + "\",\"" + "PurFourthName" + "\":" + "\"" + name4.getText().toString() + "\",\"" + "PurFourthID" + "\":" + "\"" + brn_no4.getText().toString() + "\",\"" + "PurFourthTaxNo" + "\":" + "\"" + tax_no4.getText().toString() + "\",\"" + "PurFourthContactNo" + "\":" + "\"" + contact_no4.getText().toString() + "\",\"" + "PurFourthType" + "\":" + "\"" + spinnertype4 + "\"},\"Vendor\":{\"VndrRepresentedByFirm\":\"N\",\"VndrlawyerRepresented\":\"Y\",\"VndrReqDevConsent\":\"Name\",\"VndrReceiveDevConsent\":\"Sam\",\"VndrFirstName\":\"Name1\",\"VndrFirstID\":\"Id1\",\"VndrFirstTaxNo\":\"Tax1\",\"VndrFirstContactNo\":\"9784561233\",\"VndrFirstType\":\"CORPORATE\",\"VndrSecName\":\"Name2\",\"VndrSecID\":\"Id2\",\"VndrSecTaxNo\":\"Tax2\",\"VndrSecContactNo\":\"9784561234\",\"VndrSecType\":\"INDIVIDUAL\",\"VndrThirdName\":\"Name3\",\"VndrThirdID\":\"Id3\",\"VndrThirdTaxNo\":\"Tax3\",\"VndrThirdContactNo\":\"9784561234\",\"VndrThirdType\":\"INDIVIDUAL\",\"VndrFourthName\":\"Name4\",\"VndrFourthID\":\"Id4\",\"VndrFourthTaxNo\":\"Tax4\",\"VndrFourthContactNo\":\"9784561235\",\"VndrFourthType\":\"INDIVIDUAL\"},\"Property\":{\"TitleType\":\"KEKAL\",\"CertifiedPlanNo\":\"\",\"LotNo\":\"\",\"PreviouslyKnownAs\":\"\",\"State\":\"\",\"Area\":\"\",\"BPM\":\"\",\"GovSurvyPlan\":\"\",\"LotArea\":\"12345\",\"Developer\":\"\",\"Project\":\"\",\"DevLicenseNo\":\"\",\"DevSolicitor\":\"\",\"DevSoliLoc\":\"\",\"TitleSearchDate\":\"\",\"DSCTransfer\":\"\",\"DRCTransfer\":\"\",\"FourteenADate\":\"\",\"DRTLRegistry\":\"\",\"PropertyCharged\":\"\",\"BankName\":\"\",\"Branch\":\"\",\"PAName\":\"\",\"PresentationNo\":\"\",\"ExistChargeRef\":\"\",\"ReceiptType\":\"\",\"ReceiptNo\":\"\",\"ReceiptDate\":\"\",\"PurchasePrice\":\"1452\",\"AdjValue\":\"\",\"VndrPrevSPAValue\":\"\",\"Deposit\":\"\",\"BalPurPrice\":\"\",\"LoanAmount\":\"\",\"LoanCaseNo\":\"1234\",\"DiffSum\":\"\",\"RedAmt\":\"\",\"RedDate\":\"\",\"DefRdmptSum\":\"\"},\"LoanPrinciple\":{\"ReqRedStatement\":\"\",\"RedStmtDate\":\"\",\"RedPayDate\":\"\",\"RepByFirm\":\"\",\"LoanCaseNo\":\"1234\",\"Project\":\"\",\"MasterBankName\":\"\",\"BranchName\":\"\",\"Address\":\"\",\"PAName\":\"\",\"BankRef\":\"\",\"BankInsDate\":\"2015/10/11\",\"LOFDate\":\"\",\"BankSolicitor\":\"\",\"SoliLoc\":\"\",\"SoliRef\":\"\",\"TypeofLoan\":\"\",\"TypeofFacility\":\"\",\"FacilityAmt\":\"\",\"Repaymt\":\"\",\"IntrstRate\":\"\",\"MonthlyInstmt\":\"\",\"TermLoanAmt\":\"\",\"Interest\":\"\",\"ODLoan\":\"\",\"MRTA\":\"\",\"BankGuarantee\":\"\",\"LetterofCredit\":\"\",\"TrustReceipt\":\"\",\"Others\":\"\",\"LoanDet1\":\"\",\"LoanDet2\":\"\",\"LoanDet3\":\"\",\"LoanDet4\":\"\",\"LoanDet5\":\"\"},\"LoanSubsidary\":{\"LoanDocForBankExe\":\"Exe 1\",\"FaciAgreeDate\":\"\",\"LoanDocRetFromBank\":\"\",\"DischargeofCharge\":\"\",\"FirstTypeofFacility\":\"\",\"FirstFacilityAmt\":\"\",\"FirstRepaymt\":\"\",\"FirstIntrstRate\":\"\",\"FirstMonthlyInstmt\":\"1500\",\"FirstTermLoanAmt\":\"\",\"FirstInterest\":\"\",\"FirstODLoan\":\"\",\"FirstMRTA\":\"\",\"FirstBankGuarantee\":\"\",\"FirstLetterofCredit\":\"\",\"FirstTrustReceipt\":\"\",\"FirstOthers\":\"Sample\",\"SecTypeofFacility\":\"\",\"SecFacilityAmt\":\"\",\"SecRepaymt\":\"\",\"SecIntrstRate\":\"12%\",\"SecMonthlyInstmt\":\"\",\"SecTermLoanAmt\":\"\",\"SecInterest\":\"\",\"SecODLoan\":\"\",\"SecMRTA\":\"\",\"SecBankGuarantee\":\"\",\"SecLetterofCredit\":\"\",\"SecTrustReceipt\":\"45A\",\"SecOthers\":\"\",\"ThirdTypeofFacility\":\"Sample_Data\",\"ThirdFacilityAmt\":\"\",\"ThirdRepaymt\":\"\",\"ThirdIntrstRate\":\"\",\"ThirdMonthlyInstmt\":\"\",\"ThirdTermLoanAmt\":\"587.15\",\"ThirdInterest\":\"\",\"ThirdODLoan\":\"\",\"ThirdMRTA\":\"\",\"ThirdBankGuarantee\":\"\",\"ThirdLetterofCredit\":\"\",\"ThirdTrustReceipt\":\"\",\"ThirdOthers\":\"\",\"FourthTypeofFacility\":\"Sample4\",\"FourthFacilityAmt\":\"\",\"FourthRepaymt\":\"15\",\"FourthIntrstRate\":\"\",\"FourthMonthlyInstmt\":\"\",\"FourthTermLoanAmt\":\"\",\"FourthInterest\":\"21\",\"FourthODLoan\":\"\",\"FourthMRTA\":\"\",\"FourthBankGuarantee\":\"\",\"FourthLetterofCredit\":\"\",\"FourthTrustReceipt\":\"\",\"FourthOthers\":\"\",\"FifthTypeofFacility\":\"Sample 5\",\"FifthFacilityAmt\":\"\",\"FifthRepaymt\":\"\",\"FifthIntrstRate\":\"\",\"FifthMonthlyInstmt\":\"\",\"FifthTermLoanAmt\":\"\",\"FifthInterest\":\"10\",\"FifthODLoan\":\"\",\"FifthMRTA\":\"\",\"FifthBankGuarantee\":\"\",\"FifthLetterofCredit\":\"\",\"FifthTrustReceipt\":\"Test\",\"FifthOthers\":\"\"}}]";
	String json_element = "[ { \"Case\":" + "\"" + jsonResponseconfirm.get("Case").toString() + "\", \"CaseType\": " + "\"" + jsonResponseconfirm.get("CaseType").toString() + "\", \"CaseStatus\": \"OPEN\", \"FileOpenDate\": \"10/10/201512: 00: 00AM\", \"CaseFileNo\": \"JJ/1500000001/\", \"KIV\": \"\", \"TabId\": \"6\", \"Details\": {}, \"Purchaser\": {}, \"Vendor\": {}, \"Property\": {}, \"LoanPrinciple\": {}, \"LoanSubsidary\": { \"LoanDocForBankExe\":\""+ELoanDocForBankExe.getText().toString()+"\", \"FaciAgreeDate\":\""+EFaciAgreeDate.getText().toString()+"\", \"LoanDocRetFromBank\":\""+EeditText_loan_doc_bank.getText().toString()+"\", \"DischargeofCharge\":\""+EeditText_dischargeofcharge.getText().toString()+"\", \"FirstTypeofFacility\":\""+f1Value+"\", \"FirstFacilityAmt\":\""+EeditText_fac_amount.getText().toString()+"\", \"FirstRepaymt\":\""+EeditText_repayment.getText().toString()+"\", \"FirstIntrstRate\":\""+EeditText_interestrate.getText().toString()+"\", \"FirstMonthlyInstmt\":\""+EeditText_monthly_installment.getText().toString()+"\", \"FirstTermLoanAmt\":\""+EeditText_loan_amt.getText().toString()+"\", \"FirstInterest\":\""+EeditText_interest.getText().toString()+"\", \"FirstODLoan\":\""+EeditText_od_loan.getText().toString()+"\", \"FirstMRTA\":\""+EeditText_mrta.getText().toString()+"\", \"FirstBankGuarantee\":\""+EeditText_bank_guarantee.getText().toString()+"\", \"FirstLetterofCredit\":\""+EeditText_letterofcredit.getText().toString()+"\", \"FirstTrustReceipt\":\""+EeditText_trust_receipt.getText().toString()+"\", \"FirstOthers\":\""+EeditText_others.getText().toString()+"\", \"SecTypeofFacility\":\""+f2Value+"\", \"SecFacilityAmt\":\""+EeditText_fac_amount1.getText().toString()+"\", \"SecRepaymt\":\""+EeditText_repayment1.getText().toString()+"\", \"SecIntrstRate\":\""+EeditText_interestrate1.getText().toString()+"\", \"SecMonthlyInstmt\":\""+EeditText_monthly_installment1.getText().toString()+"\", \"SecTermLoanAmt\":\""+EeditText_loan_amt1.getText().toString()+"\", \"SecInterest\":\""+EeditText_interest1.getText().toString()+"\", \"SecODLoan\":\""+EeditText_od_loan1.getText().toString()+"\", \"SecMRTA\":\""+EeditText_mrta1.getText().toString()+"\", \"SecBankGuarantee\":\""+EeditText_bank_guarantee1.getText().toString()+"\", \"SecLetterofCredit\":\""+EeditText_letterofcredit1.getText().toString()+"\", \"SecTrustReceipt\":\""+EeditText_trust_receipt1.getText().toString()+"\", \"SecOthers\":\""+EeditText_others1.getText().toString()+"\", \"ThirdTypeofFacility\":\""+f3Value+"\", \"ThirdFacilityAmt\":\""+EeditText_fac_amount2.getText().toString()+"\", \"ThirdRepaymt\":\""+EeditText_repayment2.getText().toString()+"\", \"ThirdIntrstRate\":\""+EeditText_interestrate2.getText().toString()+"\", \"ThirdMonthlyInstmt\":\""+EeditText_monthly_installment2.getText().toString()+"\", \"ThirdTermLoanAmt\":\""+EeditText_loan_amt2.getText().toString()+"\", \"ThirdInterest\":\""+EeditText_interest2.getText().toString()+"\", \"ThirdODLoan\":\""+EeditText_od_loan2.getText().toString()+"\", \"ThirdMRTA\":\""+EeditText_mrta2.getText().toString()+"\", \"ThirdBankGuarantee\":\""+EeditText_bank_guarantee2.getText().toString()+"\", \"ThirdLetterofCredit\":\""+EeditText_letterofcredit2.getText().toString()+"\", \"ThirdTrustReceipt\":\""+EeditText_trust_receipt2.getText().toString()+"\", \"ThirdOthers\":\""+EeditText_others2.getText().toString()+"\", \"FourthTypeofFacility\":\""+f4Value+"\", \"FourthFacilityAmt\":\""+EeditText_fac_amount3.getText().toString()+"\", \"FourthRepaymt\":\""+EeditText_repayment3.getText().toString()+"\", \"FourthIntrstRate\":\""+EeditText_interestrate3.getText().toString()+"\", \"FourthMonthlyInstmt\":\""+EeditText_monthly_installment3.getText().toString()+"\", \"FourthTermLoanAmt\":\""+EeditText_loan_amt3.getText().toString()+"\", \"FourthInterest\":\""+EeditText_interest3.getText().toString()+"\", \"FourthODLoan\":\""+EeditText_od_loan3.getText().toString()+"\", \"FourthMRTA\":\""+EeditText_mrta3.getText().toString()+"\", \"FourthBankGuarantee\":\""+EeditText_bank_guarantee3.getText().toString()+"\", \"FourthLetterofCredit\":\""+EeditText_letterofcredit3.getText().toString()+"\", \"FourthTrustReceipt\":\""+EeditText_trust_receipt3.getText().toString()+"\", \"FourthOthers\":\""+EeditText_others3.getText().toString()+"\", \"FifthTypeofFacility\":\""+f5Value+"\", \"FifthFacilityAmt\":\""+EeditText_fac_amount4.getText().toString()+"\", \"FifthRepaymt\":\""+EeditText_repayment4.getText().toString()+"\", \"FifthIntrstRate\":\""+EeditText_interestrate4.getText().toString()+"\", \"FifthMonthlyInstmt\":\""+EeditText_monthly_installment4.getText().toString()+"\", \"FifthTermLoanAmt\":\""+EeditText_loan_amt4.getText().toString()+"\", \"FifthInterest\":\""+EeditText_interest4.getText().toString()+"\", \"FifthODLoan\":\""+EeditText_od_loan4.getText().toString()+"\", \"FifthMRTA\":\""+EeditText_mrta4.getText().toString()+"\", \"FifthBankGuarantee\":\""+EeditText_bank_guarantee4.getText().toString()+"\", \"FifthLetterofCredit\":\""+EeditText_letterofcredit4.getText().toString()+"\", \"FifthTrustReceipt\":\""+EeditText_trust_receipt4.getText().toString()+"\", \"FifthOthers\":\""+EeditText_others4.getText().toString()+"\" } } ]";
	
	
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
		
		//list.put(valuesObject);
		
		
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
			Intent iAddBack = new Intent(ProcesscaseLoanSubsidiary.this, ProcessCaseProcessTab.class);
			startActivity(iAddBack);
			Toast.makeText(ProcesscaseLoanSubsidiary.this, messageDisplay, Toast.LENGTH_SHORT).show();
		} else {
			Toast.makeText(ProcesscaseLoanSubsidiary.this, messageDisplay, Toast.LENGTH_SHORT).show();

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


private void setallvalues(String arg2) {

    JSONArray jObj = null;
    try {
        jObj = new JSONArray(arg2.toString());

        for (int i = 0; i < jObj.length(); i++) {
            JSONObject jsonobject = jObj.getJSONObject(i);
            file_open_date.setText(jsonobject.getString("FileOpenDate"));
            case_fileno.setText(jsonobject.getString("CaseFileNo"));
            case_type.setText(jsonobject.getString("CaseType"));
            Scase_status = jsonobject.getString("CaseStatus");
            Skiv = jsonobject.getString("KIV");

            JSONObject obj1 = jsonobject.getJSONObject("LoanSubsidary");
            
            System.out.println("LoanSubsidary");
            System.out.println(obj1);

            ELoanDocForBankExe.setText(obj1.getString("LoanDocForBankExe"));
            EFaciAgreeDate.setText(obj1.getString("FaciAgreeDate"));
            EeditText_loan_doc_bank.setText(obj1.getString("LoanDocRetFromBank"));
            EeditText_dischargeofcharge.setText(obj1.getString("DischargeofCharge"));
           // EeditText_type_facility.setText(obj1.getString("FirstTypeofFacility"));
            EeditText_fac_amount.setText(obj1.getString("FirstFacilityAmt"));
            EeditText_repayment.setText(obj1.getString("FirstRepaymt"));
            EeditText_interestrate.setText(obj1.getString("FirstIntrstRate"));
            EeditText_monthly_installment.setText(obj1.getString("FirstMonthlyInstmt"));
            EeditText_loan_amt.setText(obj1.getString("FirstTermLoanAmt"));
            EeditText_interest.setText(obj1.getString("FirstInterest"));
            EeditText_od_loan.setText(obj1.getString("FirstODLoan"));
            EeditText_mrta.setText(obj1.getString("FirstMRTA"));
            EeditText_bank_guarantee.setText(obj1.getString("FirstBankGuarantee"));
            EeditText_letterofcredit.setText(obj1.getString("FirstLetterofCredit"));
            EeditText_trust_receipt.setText(obj1.getString("FirstTrustReceipt"));
            EeditText_others.setText(obj1.getString("FirstOthers"));
            //EeditText_type_facility1.setText(obj1.getString("SecTypeofFacility"));
            EeditText_fac_amount1.setText(obj1.getString("SecFacilityAmt"));
            EeditText_repayment1.setText(obj1.getString("SecRepaymt"));
            EeditText_interestrate1.setText(obj1.getString("SecIntrstRate"));
            EeditText_monthly_installment1.setText(obj1.getString("SecMonthlyInstmt"));
            EeditText_loan_amt1.setText(obj1.getString("SecTermLoanAmt"));
            EeditText_interest1.setText(obj1.getString("SecInterest"));
            EeditText_od_loan1.setText(obj1.getString("SecODLoan"));
            EeditText_mrta1.setText(obj1.getString("SecMRTA"));
            EeditText_bank_guarantee1.setText(obj1.getString("SecBankGuarantee"));
            EeditText_letterofcredit1.setText(obj1.getString("SecLetterofCredit"));
            EeditText_trust_receipt1.setText(obj1.getString("SecTrustReceipt"));
            EeditText_others1.setText(obj1.getString("SecOthers"));
            //EeditText_type_facility2.setText(obj1.getString("ThirdTypeofFacility"));
            EeditText_fac_amount2.setText(obj1.getString("ThirdFacilityAmt"));
            EeditText_repayment2.setText(obj1.getString("ThirdRepaymt"));
            EeditText_interestrate2.setText(obj1.getString("ThirdIntrstRate"));
            EeditText_monthly_installment2.setText(obj1.getString("ThirdMonthlyInstmt"));
            EeditText_loan_amt2.setText(obj1.getString("ThirdTermLoanAmt"));
            EeditText_interest2.setText(obj1.getString("ThirdInterest"));
            EeditText_od_loan2.setText(obj1.getString("ThirdODLoan"));
            EeditText_mrta2.setText(obj1.getString("ThirdMRTA"));
            EeditText_bank_guarantee2.setText(obj1.getString("ThirdBankGuarantee"));
            EeditText_letterofcredit2.setText(obj1.getString("ThirdLetterofCredit"));
            EeditText_trust_receipt2.setText(obj1.getString("ThirdTrustReceipt"));
            EeditText_others2.setText(obj1.getString("ThirdOthers"));
            //EeditText_type_facility3.setText(obj1.getString("FourthTypeofFacility"));
            EeditText_fac_amount3.setText(obj1.getString("FourthFacilityAmt"));
            EeditText_repayment3.setText(obj1.getString("FourthRepaymt"));
            EeditText_interestrate3.setText(obj1.getString("FourthIntrstRate"));
            EeditText_monthly_installment3.setText(obj1.getString("FourthMonthlyInstmt"));
            EeditText_loan_amt3.setText(obj1.getString("FourthTermLoanAmt"));
            EeditText_interest3.setText(obj1.getString("FourthInterest"));
            EeditText_od_loan3.setText(obj1.getString("FourthODLoan"));
            EeditText_mrta3.setText(obj1.getString("FourthMRTA"));
            EeditText_bank_guarantee3.setText(obj1.getString("FourthBankGuarantee"));
            EeditText_letterofcredit3.setText(obj1.getString("FourthLetterofCredit"));
            EeditText_trust_receipt3.setText(obj1.getString("FourthTrustReceipt"));
            EeditText_others3.setText(obj1.getString("FourthOthers"));
           // EeditText_type_facility4.setText(obj1.getString("FifthTypeofFacility"));
            EeditText_fac_amount4.setText(obj1.getString("FifthFacilityAmt"));
            EeditText_repayment4.setText(obj1.getString("FifthRepaymt"));
            EeditText_interestrate4.setText(obj1.getString("FifthIntrstRate"));
            EeditText_monthly_installment4.setText(obj1.getString("FifthMonthlyInstmt"));
            EeditText_loan_amt4.setText(obj1.getString("FifthTermLoanAmt"));
            EeditText_interest4.setText(obj1.getString("FifthInterest"));
            EeditText_od_loan4.setText(obj1.getString("FifthODLoan"));
            EeditText_mrta4.setText(obj1.getString("FifthMRTA"));
            EeditText_bank_guarantee4.setText(obj1.getString("FifthBankGuarantee"));
            EeditText_letterofcredit4.setText(obj1.getString("FifthLetterofCredit"));
            EeditText_trust_receipt4.setText(obj1.getString("FifthTrustReceipt"));
            EeditText_others4.setText(obj1.getString("FifthOthers"));
            
            
            Ssf1 = obj1.getString("FirstTypeofFacility");
            Ssf2 = obj1.getString("SecTypeofFacility");
            Ssf3 = obj1.getString("ThirdTypeofFacility");
            Ssf4 = obj1.getString("FourthTypeofFacility");
            Ssf5 = obj1.getString("FifthTypeofFacility");
            
            dropdownstatus();
            dropdownKIV();
            dropdownf1();


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

                sAdapPROJ = new SimpleAdapter(ProcesscaseLoanSubsidiary.this, jsonArraylist, R.layout.spinner_item,
                        new String[]{"Id_T", "Name_T"}, new int[]{R.id.Id, R.id.Name});

                spinner_case_status.setAdapter(sAdapPROJ);

                for (int j = 0; j < jsonArraylist.size(); j++) {
                    if (jsonArraylist.get(j).get("Id_T").equals(Scase_status)) {
                    	spinner_case_status.setSelection(j);
						break;
					}
				}

            } catch (JSONException e) {

                e.printStackTrace();
            }
        }
    });
}

@Override
public void onClick(View v) {
	if (v == details_btn) {
		 Intent to_purchaser = new Intent(ProcesscaseLoanSubsidiary.this, ProcessCaseDetails.class);
           startActivity(to_purchaser);
	}
	if (v == purchaser_btn) {
        Intent to_purchaser = new Intent(ProcesscaseLoanSubsidiary.this, ProcessCasePurchaser.class);
        startActivity(to_purchaser);
    }
    if (v == vendor_btn) {
        Intent to_vendor = new Intent(ProcesscaseLoanSubsidiary.this, ProcessCaseVendor.class);
        startActivity(to_vendor);
    }
    if (v == property_btn) {
        Intent to_loan_pricipal = new Intent(ProcesscaseLoanSubsidiary.this, ProcessCaseProperty.class);
        startActivity(to_loan_pricipal);
    }
    if (v == loan_principal_btn) {
        Intent to_loan_pricipal = new Intent(ProcesscaseLoanSubsidiary.this, ProcessCaseLoanPrincipal.class);
        startActivity(to_loan_pricipal);
    }
    if (v == loan_subsidary_btn) {
        Intent to_loan_subsidiary = new Intent(ProcesscaseLoanSubsidiary.this, ProcesscaseLoanSubsidiary.class);
        startActivity(to_loan_subsidiary);
    }
    if (v == process_btn) {
        Intent to_loan_subsidiary = new Intent(ProcesscaseLoanSubsidiary.this, ProcessCaseProcessTab.class);
        startActivity(to_loan_subsidiary);
    }
   
    EeditText_dischargeofcharge.setOnClickListener(this);
    if(v == ELoanDocForBankExe)
    {
		 new DatePickerDialog(ProcesscaseLoanSubsidiary.this, ELoanDocForBankExe1, myCalendar
                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
    }	
    if(v == EFaciAgreeDate)
    {
		 new DatePickerDialog(ProcesscaseLoanSubsidiary.this, EFaciAgreeDate1, myCalendar
                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
    }	
    if(v == EeditText_loan_doc_bank)
    {
		 new DatePickerDialog(ProcesscaseLoanSubsidiary.this, EeditText_loan_doc_bank1, myCalendar
                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
    }	
    if(v == EeditText_dischargeofcharge)
    {
		 new DatePickerDialog(ProcesscaseLoanSubsidiary.this, EeditText_dischargeofcharge1, myCalendar
                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
    }	
    if(v == walkin)
    {
    	Intent i = new Intent(ProcesscaseLoanSubsidiary.this, WalkInActivity.class);
		startActivity(i);
    }
    if (v == confirm_allvalues_btn) {
    	try {
			confirm_allvalues();
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}        
    }
}

DatePickerDialog.OnDateSetListener ELoanDocForBankExe1 = new DatePickerDialog.OnDateSetListener() {

    @Override
    public void onDateSet(DatePicker view, int year, int monthOfYear,
            int dayOfMonth) {
        // TODO Auto-generated method stub
        myCalendar.set(Calendar.YEAR, year);
        myCalendar.set(Calendar.MONTH, monthOfYear);
        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
        ELoanDocForBankExe.setText(sdf.format(myCalendar.getTime()));
    }

};

DatePickerDialog.OnDateSetListener EFaciAgreeDate1 = new DatePickerDialog.OnDateSetListener() {

    @Override
    public void onDateSet(DatePicker view, int year, int monthOfYear,
            int dayOfMonth) {
        // TODO Auto-generated method stub
        myCalendar.set(Calendar.YEAR, year);
        myCalendar.set(Calendar.MONTH, monthOfYear);
        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
        EFaciAgreeDate.setText(sdf.format(myCalendar.getTime()));
    }

};

DatePickerDialog.OnDateSetListener EeditText_dischargeofcharge1 = new DatePickerDialog.OnDateSetListener() {

    @Override
    public void onDateSet(DatePicker view, int year, int monthOfYear,
            int dayOfMonth) {
        // TODO Auto-generated method stub
        myCalendar.set(Calendar.YEAR, year);
        myCalendar.set(Calendar.MONTH, monthOfYear);
        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
        EeditText_dischargeofcharge.setText(sdf.format(myCalendar.getTime()));
    }

};
DatePickerDialog.OnDateSetListener EeditText_loan_doc_bank1 = new DatePickerDialog.OnDateSetListener() {

    @Override
    public void onDateSet(DatePicker view, int year, int monthOfYear,
            int dayOfMonth) {
        // TODO Auto-generated method stub
        myCalendar.set(Calendar.YEAR, year);
        myCalendar.set(Calendar.MONTH, monthOfYear);
        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
        EeditText_loan_doc_bank.setText(sdf.format(myCalendar.getTime()));
    }

};

public boolean dispatchTouchEvent(MotionEvent ev) {
	InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
	imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
	return super.dispatchTouchEvent(ev);

}

}
