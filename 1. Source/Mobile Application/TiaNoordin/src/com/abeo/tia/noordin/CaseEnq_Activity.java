package com.abeo.tia.noordin;


import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collection;
import java.util.HashMap;
import java.util.Locale;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.DatePickerDialog.OnDateSetListener;
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
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class CaseEnq_Activity extends BaseActivity {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx/SPA_IndividualSearch

	// Find list of Individual enquiry list web method
	private final String METHOD_SEARCH_INDIVIDUAL = "SPA_IndividualSearch";

	// Find list of Corporate enquiry list web method
	private final String METHOD_NAME_SPA_CaseEnquiry = "SPA_CaseEnquiry";

	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	
	ArrayList<HashMap<String, String>> jsonCaselist;

	// Find Button
	Button buttonSearch;
	public String tag = "";

	// Find Edit Text field
	EditText EFODF, EFODT, ECFN,ECtype,ECLN,ECAF,ECAT;
	
	Calendar myCalendar = Calendar.getInstance();


	// Find String passing in corporate UI
	String codeC = "", docEntryC = "", compNameC = "", bRNNoC = "", taxNoC = "", OfficeNoC = "", iDAddress1C = "",
			iDAddress2C = "", iDAddress3C = "", iDAddress4C = "", iDAddress5C = "", corresAddr1C = "";
	
	// Find String for corporate case list
			String caseFileNo = "", relatedFileNo = "", branchCode = "", fileOpenedDate = "", iC = "", caseType = "",
					clientName = "", bankName = "", branch = "", lOTNo = "", caseAmount = "", userCode = "", status = "",
					fileClosedDate = "";
			

	// Find JSON Array
	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_case_enquiry);
		// load titles from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		// Find Set Function
		set(navMenuTitles, navMenuIcons);
		// Find Edit by Id
		
		// Find the SharedPreferences Firstname
				SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);		
				String FirName = FirstName.getString("FIRSETNAME", "");
				TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
				welcome.setText("Welcome "+FirName);
		

		// Find button by Id, EFODT, ,,,;
		EFODF = (EditText) findViewById(R.id.FODF);
		EFODT = (EditText) findViewById(R.id.FODT);
		ECFN = (EditText) findViewById(R.id.CFN);
		ECtype = (EditText) findViewById(R.id.Ctype);
		ECLN = (EditText) findViewById(R.id.CLN);
		ECAF = (EditText) findViewById(R.id.CAF);
		ECAT = (EditText) findViewById(R.id.CAT);
		
		ECtype.setText("SPA");
		ECtype.setEnabled(false);
		
		EFODF.setOnClickListener(new OnClickListener() {

	        @Override
	        public void onClick(View v) {
	           
				// TODO Auto-generated method stub
	            new DatePickerDialog(CaseEnq_Activity.this, date, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
	        }
	    });
		
		EFODT.setOnClickListener(new OnClickListener() {

	        @Override
	        public void onClick(View v) {
	           
				// TODO Auto-generated method stub
	            new DatePickerDialog(CaseEnq_Activity.this, datew, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
	        }
	    });
		
		
		buttonSearch = (Button) findViewById(R.id.BTN_Search);
		buttonSearch.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				//Toast.makeText(CaseEnq_Activity.this, "Walk-in individual Search button click", Toast.LENGTH_LONG).show();
				Log.e(tag, "Walk-in individual search Button Clicked");

				// call edit webservice for property details
				searchDetails();

			}
		});
		

	}
	
	DatePickerDialog.OnDateSetListener date = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        updateLabel();
	    }

	};
	
	DatePickerDialog.OnDateSetListener datew = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        updateLabel2();
	    }

	};

	

private void updateLabel2() {

    String myFormat = "yyyy/MM/dd"; //In which you need put here
    SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());

    
    EFODT.setText(sdf.format(myCalendar.getTime()));
    }



private void updateLabel() {

    String myFormat = "yyyy/MM/dd"; //In which you need put here
    SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());

    
	EFODF.setText(sdf.format(myCalendar.getTime()));
    }

	public void searchDetails() {
		

		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		
			
			EFODF = (EditText) findViewById(R.id.FODF);
			EFODT = (EditText) findViewById(R.id.FODT);
			ECFN = (EditText) findViewById(R.id.CFN);
			ECtype = (EditText) findViewById(R.id.Ctype);
			ECLN = (EditText) findViewById(R.id.CLN);
			ECAF = (EditText) findViewById(R.id.CAF);
			ECAT = (EditText) findViewById(R.id.CAT);

			String SFODF = EFODF.getText().toString();
			String SFODT = EFODT.getText().toString();
			String SCFN = ECFN.getText().toString();
			String SCtype = ECtype.getText().toString();
			String SCLN = ECLN.getText().toString();
			String SCAF = ECAF.getText().toString();
			String SCAT = ECAT.getText().toString();

				
			try {
					jsonObject.put("FileOpenDateFrom", SFODF);				
					jsonObject.put("FileOpenDateTo", SFODT);
					jsonObject.put("CaseFileNo", SCFN);
					jsonObject.put("CaseType", SCtype);
					jsonObject.put("ClientName", SCLN);
					jsonObject.put("AmountFrom", SCAF);
					jsonObject.put("AmountTo", SCAT);
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				
						
						
						
						
						
						RequestParams params = new RequestParams();
						params.put("sJsonInput", jsonObject.toString());
						System.out.println(params);

						RestService.post(METHOD_NAME_SPA_CaseEnquiry, params, new BaseJsonHttpResponseHandler<String>() {

							@Override
							public void onFailure(int arg0, Header[] arg1,
									Throwable arg2, String arg3, String arg4) {
								// TODO Auto-generated method stub
								
							}

							@Override
							public void onSuccess(int arg0, Header[] arg1, String arg2,
									String arg3) {
								
								
						
								try {
										
									System.out.println(arrayResponse.length());
									jsonCaselist = new ArrayList<HashMap<String, String>>();
								for (int i = 0; i < arrayResponse.length(); i++) {
									
										jsonResponse = arrayResponse.getJSONObject(i);
									

									/*
									 * { CaseFileNo: "1500000002", RelatedFileNo: "",
									 * BranchCode: "", FileOpenedDate:
									 * "8/26/2015 12:00:00 AM", IC: "3", CaseType:
									 * "SPA", ClientName: "CHAI SEEN \TIA & NOORDIN \",
									 * BankName: "", Branch: "", LOTNo: "", CaseAmount:
									 * "", UserCode: "", Status: "OPEN", FileClosedDate:
									 * "8/26/2015 12:00:00 AM" },
									 */
									caseFileNo = jsonResponse.getString("CaseFileNo").toString();
									relatedFileNo = jsonResponse.getString("RelatedFileNo").toString();
									branchCode = jsonResponse.getString("BranchCode").toString();
									fileOpenedDate = jsonResponse.getString("FileOpenedDate").toString();
									iC = jsonResponse.getString("IC").toString();
									caseType = jsonResponse.getString("CaseType").toString();
									clientName = jsonResponse.getString("ClientName").toString();
									bankName = jsonResponse.getString("BankName").toString();
									branch = jsonResponse.getString("Branch").toString();
									lOTNo = jsonResponse.getString("LOTNo").toString();
									caseAmount = jsonResponse.getString("CaseAmount").toString();
									userCode = jsonResponse.getString("UserCode").toString();
									status = jsonResponse.getString("Status").toString();
									fileClosedDate = jsonResponse.getString("FileClosedDate").toString();

									// SEND JSON DATA INTO CASELIST
									HashMap<String, String> caseListProperty = new HashMap<String, String>();

									// Send JSON Data to list activity
									System.out.println("SEND JSON CORPORATE CASE LIST");

									caseListProperty.put("CaseFileNo_List", caseFileNo);
									System.out.println(caseFileNo);
									caseListProperty.put("RelatedFileNo_List", relatedFileNo);
									System.out.println(relatedFileNo);
									caseListProperty.put("BranchCode_List", branchCode);
									System.out.println(branchCode);
									caseListProperty.put("FileOpenedDate_List", fileOpenedDate);
									System.out.println(fileOpenedDate);
									caseListProperty.put("IC_List", iC);
									System.out.println(iC);
									caseListProperty.put("CaseType_List", caseType);
									System.out.println(caseType);
									caseListProperty.put("ClientName_List", clientName);
									System.out.println(clientName);
									caseListProperty.put("BankName_List", bankName);
									System.out.println(bankName);
									caseListProperty.put("Branch_List", branch);
									System.out.println(branch);
									caseListProperty.put("LOTNo_List", lOTNo);
									System.out.println(lOTNo);
									caseListProperty.put("CaseAmount_List", caseAmount);
									System.out.println(caseAmount);
									caseListProperty.put("UserCode_List", userCode);
									System.out.println(userCode);
									caseListProperty.put("Status_List", status);
									System.out.println(status);
									caseListProperty.put("FileClosedDate", fileClosedDate);
									System.out.println(fileClosedDate);
									System.out.println(" END SEND JSON CORPORATE CASE LIST");

									jsonCaselist.add(caseListProperty);
									System.out.println("JSON CASELIST");
									System.out.println(jsonCaselist);

								}
								
								} catch (JSONException e) {
									// TODO Auto-generated catch block
									e.printStackTrace();
								}
								
								Intent i = new Intent(CaseEnq_Activity.this, CaseEnqActivity.class);
								i.putExtra("ProjectJsonList", jsonCaselist);
								System.out.println(jsonResponse);
								startActivity(i);
							}

							@Override
							protected String parseResponse(String arg0, boolean arg1)
									throws Throwable {
								// Get Json response
								arrayResponse = new JSONArray(arg0);
								for (int j = 0; j < arrayResponse.length(); j++) {
									jsonResponse = arrayResponse.getJSONObject(j);
								}

								System.out.println("ffffffffffff");
								System.out.println(arg0);
								return null;
							}
						});
						
						

	}
	
	public boolean dispatchTouchEvent(MotionEvent ev) {	       
        InputMethodManager imm = (InputMethodManager)getSystemService(Context.
                INPUT_METHOD_SERVICE);
imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
        return super.dispatchTouchEvent(ev);

        } 

	}
