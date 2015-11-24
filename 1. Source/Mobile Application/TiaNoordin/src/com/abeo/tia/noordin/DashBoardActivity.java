package com.abeo.tia.noordin;

import java.util.ArrayList;
import java.util.HashMap;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;
import com.navigationdrawer.NavDrawerItem;
import com.navigationdrawer.NavDrawerListAdapter;

import abeo.tia.noordin.R;
import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.ActionBarDrawerToggle;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

/**
 * @author Satya
 *
 */
@SuppressWarnings("deprecation")
@SuppressLint("NewApi")
public class DashBoardActivity extends BaseActivity {
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	// Find Json Response
	private ProgressBar ProgBar_Tb_Tm_NewCaess, ProgBar_Tb_Tm_ClosedCases, ProgBar_Tb_Lm_NewCaess,
			ProgBar_Tb_Lm_ClosedCases;
	private Handler mHandler = new Handler();;
	private double mProgressStatus = 0;
	private EditText EditText_TB_TM_NEWCASE, EditText_TB_TM_CLOSEDCASES, EditText_TB_LM_NEWCASE,
			EditText_TB_LM_CLOSEDCASES;
	private EditText EditText_YS_TM_TURNAROUND, EditText_YS_TM_TOTALOUTPUT, EditText_YS_LM_TURAROUND,
			EditText_YS_LM_TOTALOUTPUT;
	private Button Button_PRIORITY, Button_ACTION, Button_OPEN;
	private String tb_tm_new = "", tb_tm_closed = "", tb_lm_new = "", tb_lm_closed = "";
	private String ys_tm_turnaround = "", ys_tm_totaloutput = "", ys_lm_turnaroud = "", ys_lm_totaloutput = "";
	private String priority_response = "", action_response = "", open_response = "";
	String tb_tm_new_value = null, tb_tm_closed_value = null, tb_lm_new_value = null, tb_lm_closed_value = null, FirstName = null, tb_tm_NewCases = null,
	tb_tm_ClosedCases = null,	tb_lm_NewCases = null,	tb_lm_ClosedCases = null,ys_lm_turnaround = "";
	Button walkin;
	public String tag = "";
	double tb_tm_new_value_num = 0, tb_tm_close_value_num = 0, tb_lm_new_value_num = 0, tb_lm_close_value_num = 0;
	
	// Find String passing in corporate UI Response
		String codeCResponse = "", docEntryCResponse = "", compNameCResponse = "", bRNNoCResponse = "", taxNoCResponse = "",
				OfficeNoCResponse = "", iDAddress1CResponse = "", iDAddress2CResponse = "", iDAddress3CResponse = "",
				iDAddress4CResponse = "", iDAddress5CResponse = "", corresAddr1CResponse = "", corresAddr2CResponse = "",
				corresAddr3CResponse = "", corresAddr4CResponse = "", corresAddr5CResponse = "", addressToUseCResponse = "",
				lastUpdatedOnCResponse = "", dirCode1CResponse = "", dirName1CResponse = "", dirContactNum1CResponse = "";
		
		// Find String for corporate case list
		String caseFileNo = "", relatedFileNo = "", branchCode = "", fileOpenedDate = "", iC = "", caseType = "",
				clientName = "", bankName = "", branch = "", lOTNo = "", caseAmount = "", userCode = "", status = "",
				fileClosedDate = "";
		
		// Find corporate related case web method
		private final String METHOD_RELATEDCASE_CORPORATE = "SPA_RelatedCases";
	
	private final String METHOD_NAME = "SPA_ValidateUser";
	
	private final String METHOD_Opencase = "SPA_OpenCases";
	private final String METHOD_Actioncase = "SPA_ActionCases";
	private final String METHOD_PriorityCases = "SPA_PriorityCases";
	
	ProgressDialog dialog = null;
	JSONArray arrayResponse;
	JSONObject jsonResponse;
	ArrayList<HashMap<String, String>> jsonCaselist;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_dashboard);
		
		
		

		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items); // load
		// titles
		// from
		// strings.xml

		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);// load
		// icons
		// from
		// strings.xml

		set(navMenuTitles, navMenuIcons);
		// Find Button ID
		walkin = (Button) findViewById(R.id.button_D_walkin);

		walkin.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// Find Walk-in UI
				//Toast.makeText(DashBoardActivity.this, "Walk-in button click", Toast.LENGTH_LONG).show();
				Log.e(tag, "Walk-in Button Clicked");
				Intent i = new Intent(DashBoardActivity.this, WalkInActivity.class);
				startActivity(i);
			}
		});
		Button_PRIORITY = (Button) findViewById(R.id.button_prority);
		Button_PRIORITY.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// Find Walk-in UI
				//Toast.makeText(DashBoardActivity.this, "Outstanding button click", Toast.LENGTH_LONG).show();				
				PriorityCases();
			}
		});
		Button_ACTION = (Button) findViewById(R.id.button_action);
		Button_ACTION.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// Find Walk-in UI
				//Toast.makeText(DashBoardActivity.this, "Action button click", Toast.LENGTH_LONG).show();
				
				ActionCases();
			}
		});
		Button_OPEN = (Button) findViewById(R.id.button_open);
		Button_OPEN.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// Find Walk-in UI
				//Toast.makeText(DashBoardActivity.this, "Open button click", Toast.LENGTH_LONG).show();
				Opencase();
			}
		});

		// Find JSON DATA in Bundle
		Intent bundle = getIntent();
userLogin();


			
	}
	
	
	
	protected void Opencase() {
		/*
		 * { "PropertyCode": "", "RelatedPartyCode": "000000000002", "CallFrom":
		 * "RELATEDPARTY", "Category": "SPA" }
		 */
		
		// Find the SharedPreferences value
					SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
					System.out.println("LOGINyyyyy DATA");
					String user_name = prefLoginReturn.getString("sUserName", "");
					

					
		try {

			// Passing value in JSON format in first 8-fields
			JSONObject jsonObject = new JSONObject();

			jsonObject.put("sUserName", user_name);
			
			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_Opencase, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					System.out.println("Open Case list OnFailure");
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					System.out.println("Open case list OnSuccess");

					// Find Response for ListView
					try {

						arrayResponse = new JSONArray(arg2);
						// Create new list
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

					} catch (JSONException e) { // TODO Auto-generated catc
												// block
						e.printStackTrace();
					}
					//Toast.makeText(DashBoardActivity.this, "Case Item Found", Toast.LENGTH_SHORT).show();
					Intent intentList = new Intent(DashBoardActivity.this, OpenActivity.class);
					intentList.putExtra("ProjectJsonList", jsonCaselist);
					startActivity(intentList);
					System.out.println(arg2);

				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					System.out.println("Open Case list OnParseResponse");
					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println(arg0);
					return null;
				}
			});

		} catch (

		JSONException e)

		{
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	protected void PriorityCases() {
		/*
		 * { "PropertyCode": "", "RelatedPartyCode": "000000000002", "CallFrom":
		 * "RELATEDPARTY", "Category": "SPA" }
		 */
		
		// Find the SharedPreferences value
					SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
					System.out.println("LOGINyyyyy DATA");
					String user_name = prefLoginReturn.getString("sUserName", "");
					

					
		try {

			// Passing value in JSON format in first 8-fields
			JSONObject jsonObject = new JSONObject();

			jsonObject.put("sUserName", user_name);
			
			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_PriorityCases, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					System.out.println("PriorityCases list OnFailure");
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					System.out.println("Priority case list OnSuccess");

					// Find Response for ListView
					try {

						arrayResponse = new JSONArray(arg2);
						// Create new list
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

					} catch (JSONException e) { // TODO Auto-generated catc
												// block
						e.printStackTrace();
					}
					//Toast.makeText(DashBoardActivity.this, "Case Item Found", Toast.LENGTH_SHORT).show();
					Intent intentList = new Intent(DashBoardActivity.this, OutStandingActivity.class);
					intentList.putExtra("ProjectJsonList", jsonCaselist);
					startActivity(intentList);
					System.out.println(arg2);

				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					System.out.println("PriorityCases list OnParseResponse");
					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println(arg0);
					return null;
				}
			});

		} catch (

		JSONException e)

		{
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	protected void ActionCases() {
		/*
		 * { "PropertyCode": "", "RelatedPartyCode": "000000000002", "CallFrom":
		 * "RELATEDPARTY", "Category": "SPA" }
		 */
		
		// Find the SharedPreferences value
					SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
					System.out.println("LOGINyyyyy DATA");
					String user_name = prefLoginReturn.getString("sUserName", "");
					

					
		try {

			// Passing value in JSON format in first 8-fields
			JSONObject jsonObject = new JSONObject();

			jsonObject.put("sUserName", user_name);
			
			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_Actioncase, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					System.out.println("Property Case list OnFailure");
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					System.out.println("Property case list OnSuccess");

					// Find Response for ListView
					try {

						arrayResponse = new JSONArray(arg2);
						// Create new list
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

					} catch (JSONException e) { // TODO Auto-generated catc
												// block
						e.printStackTrace();
					}
					//Toast.makeText(DashBoardActivity.this, "Case Item Found", Toast.LENGTH_SHORT).show();
					Intent intentList = new Intent(DashBoardActivity.this, ActionActivity.class);
					intentList.putExtra("ProjectJsonList", jsonCaselist);
					startActivity(intentList);
					System.out.println(arg2);

				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					System.out.println("ActionActivity Case list OnParseResponse");
					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println(arg0);
					return null;
				}
			});

		} catch (

		JSONException e)

		{
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	
public void dotask()
{
	
	//priority_response = bundle.getStringExtra("PRIORITY");
	System.out.println(priority_response);
	Button_PRIORITY.setText(priority_response);
	
	
	//action_response = bundle.getStringExtra("ACTION");
	System.out.println(action_response);
	Button_ACTION.setText(action_response);
	
	
	//open_response = bundle.getStringExtra("OPEN");
	System.out.println(open_response);
	Button_OPEN.setText(open_response);
	
	
	// Find Progress bar value for JSONResponse
			//tb_tm_new_value = bundle.getStringExtra("TB_TM_NewCasesPro");
			System.out.println("Thom Test");
			System.out.println(tb_tm_new_value);

			//tb_tm_closed_value = bundle.getStringExtra("TB_TM_ClosedCasesPro");
			System.out.println(tb_tm_closed_value);

			//tb_lm_new_value = bundle.getStringExtra("TB_LM_NewCasesPro");
			System.out.println(tb_lm_new_value);

			//tb_lm_closed_value = bundle.getStringExtra("TB_LM_ClosedCasesPro");
			System.out.println(tb_lm_closed_value);
			
			//FirstName = bundle.getStringExtra("FirstName");
			System.out.println(FirstName);

			
			// Find the SharedPreferences value
						SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
						
						String FirName = FirstName.getString("FIRSETNAME", "");
						TextView welcome = (TextView)findViewById(R.id.textView_welcome);
						
						welcome.setText("Welcome "+FirName);
			
			// Find Progress Bar Tb_Tm_NewCaess
			ProgBar_Tb_Tm_NewCaess = (ProgressBar) findViewById(R.id.progressBar_tb_tm_new_case);
			
			
			TextView ys_tm_turnaroundt = (TextView)findViewById(R.id.tmtd);			
			ys_tm_turnaroundt.setText(ys_tm_turnaround);
						
			TextView ys_tm_totaloutputt = (TextView)findViewById(R.id.tmto);			
			ys_tm_totaloutputt.setText(ys_tm_totaloutput);
			
			TextView ys_lm_totaloutputt = (TextView)findViewById(R.id.lmto);			
			ys_lm_totaloutputt.setText(ys_lm_totaloutput);
						
			TextView ys_lm_turnaroundt = (TextView)findViewById(R.id.lmtd);			
			ys_lm_turnaroundt.setText(ys_lm_turnaround);

			
			TextView tb_tm_NewCasest = (TextView)findViewById(R.id.tmn);			
			tb_tm_NewCasest.setText(tb_tm_NewCases);
			
			TextView tb_tm_ClosedCasest = (TextView)findViewById(R.id.tmc);			
			tb_tm_ClosedCasest.setText(tb_tm_ClosedCases);
			
			TextView tb_lm_NewCasest = (TextView)findViewById(R.id.lmn);			
			tb_lm_NewCasest.setText(tb_lm_NewCases);
			
			TextView tb_lm_ClosedCasest = (TextView)findViewById(R.id.lmc);			
			tb_lm_ClosedCasest.setText(tb_lm_ClosedCases);
			
			

			/*
			 * double w;
			 * 
			 * try { w = new Double(input3.getText().toString()); } catch
			 * (NumberFormatException e) { w = 0; // your default value }
			 */

			// Start progress operation in a background thread
			new Thread(new Runnable() {
				public void run() {

					while (mProgressStatus <= Double.parseDouble(tb_tm_new_value)) {
						mProgressStatus += 1;

						// Update the progress bar
						mHandler.post(new Runnable() {
							public void run() {
								int i = (int) mProgressStatus;
								ProgBar_Tb_Tm_NewCaess.setProgress(i);
							}
						});

						try {
							// Display progress slowly
							Thread.sleep(200);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
					}
				}
			}).start();

			// Find Progress Bar Tb_lm_NewCaess
			ProgBar_Tb_Tm_ClosedCases = (ProgressBar) findViewById(R.id.progressBar_tb_tm_closed_case);
			/*
			 * try { tb_tm_close_value_num = Double.parseDouble(tb_tm_closed_value);
			 * System.out.println(tb_tm_close_value_num); } catch
			 * (NumberFormatException e) { //tb_tm_close_value_num = 0; }
			 */
			// Start progress operation in a background thread
			new Thread(new Runnable() {
				public void run() {
					while (mProgressStatus <= Double.parseDouble(tb_tm_closed_value)) {
						mProgressStatus += 1;

						// Update the progress bar
						mHandler.post(new Runnable() {
							public void run() {
								int i = (int) mProgressStatus;
								ProgBar_Tb_Tm_ClosedCases.setProgress(i);
							}
						});

						try {
							// Display progress slowly
							Thread.sleep(200);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
					}
				}
			}).start();

			// Find Progress Bar Tb_Lm_NewCases
			ProgBar_Tb_Lm_NewCaess = (ProgressBar) findViewById(R.id.progressBar_tb_lm_NewCases);

			/*
			 * try { tb_lm_new_value_num = Double.parseDouble(tb_lm_new_value);
			 * System.out.println(tb_lm_new_value_num); } catch
			 * (NumberFormatException e) { //tb_lm_new_value_num = 0; }
			 */
			// Start progress operation in a background thread
			new Thread(new Runnable() {
				public void run() {
					while (mProgressStatus <= Double.parseDouble(tb_lm_new_value)) {
						mProgressStatus += 1;

						// Update the progress bar
						mHandler.post(new Runnable() {
							public void run() {
								int i = (int) mProgressStatus;
								ProgBar_Tb_Lm_NewCaess.setProgress(i);
							}
						});

						try {
							// Display progress slowly
							Thread.sleep(200);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
					}
				}
			}).start();

			// Find Progress Bar Tb_Lm_NewCases
			ProgBar_Tb_Lm_ClosedCases = (ProgressBar) findViewById(R.id.progressBar_tb_lm_ClosedCases);

			/*
			 * try { tb_lm_close_value_num = Double.parseDouble(tb_lm_closed_value);
			 * System.out.println(tb_lm_close_value_num); } catch
			 * (NumberFormatException e) { //tb_lm_close_value_num = 0; }
			 */
			// Start progress operation in a background thread
			new Thread(new Runnable() {
				public void run() {
					while (mProgressStatus <= Double.parseDouble(tb_lm_closed_value)) {
						mProgressStatus += 1;

						// Update the progress bar
						mHandler.post(new Runnable() {
							public void run() {
								int i = (int) mProgressStatus;
								ProgBar_Tb_Lm_ClosedCases.setProgress(i);
							}
						});

						try {
							// Display progress slowly
							Thread.sleep(200);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
					}
				}
			}).start();
			
	

			// Find TB Edit Text for new case

			// ((EditText)
			/* findViewById(R.id.LoginNameEditText)).setFocusable(false);
			EditText_TB_TM_NEWCASE = (EditText) findViewById(R.id.editText_tb_tm_NewCase);
			tb_tm_new = bundle.getStringExtra("TB_TM_NewCases");
			System.out.println(tb_tm_new);
			EditText_TB_TM_NEWCASE.setText(tb_tm_new);

			EditText_TB_TM_CLOSEDCASES = (EditText) findViewById(R.id.editText_tb_tm_ClosedCase);
			tb_tm_closed = bundle.getStringExtra("TB_TM_ClosedCases");
			System.out.println(tb_tm_closed);
			EditText_TB_TM_CLOSEDCASES.setText(tb_tm_closed);

			EditText_TB_LM_NEWCASE = (EditText) findViewById(R.id.editText_tb_lm_NewCase);
			tb_lm_new = bundle.getStringExtra("TB_LM_NewCases");
			System.out.println(tb_lm_new);
			EditText_TB_LM_NEWCASE.setText(tb_lm_new);

			EditText_TB_LM_CLOSEDCASES = (EditText) findViewById(R.id.editText_tb_lm_ClosedCase);
			tb_lm_closed = bundle.getStringExtra("TB_LM_ClosedCases");
			System.out.println(tb_lm_closed);
			EditText_TB_LM_CLOSEDCASES.setText(tb_lm_closed);

			// Find YS Edit Text
			EditText_YS_TM_TURNAROUND = (EditText) findViewById(R.id.editText_ys_tm_turnaround);
			ys_tm_turnaround = bundle.getStringExtra("YS_TM_Turnaround");
			System.out.println(ys_tm_turnaround);
			EditText_YS_TM_TURNAROUND.setText(ys_tm_turnaround);

			EditText_YS_TM_TOTALOUTPUT = (EditText) findViewById(R.id.editText_ys_tm_total_output);
			ys_tm_totaloutput = bundle.getStringExtra("YS_TM_TOTALOUTPUT");
			System.out.println(ys_tm_totaloutput);
			EditText_YS_TM_TOTALOUTPUT.setText(ys_tm_totaloutput);

			EditText_YS_LM_TURAROUND = (EditText) findViewById(R.id.editText_ys_lm_turnaround);
			ys_lm_turnaroud = bundle.getStringExtra("YS_LM_TURNAROUND");
			System.out.println(ys_lm_turnaroud);
			EditText_YS_LM_TURAROUND.setText(ys_lm_turnaroud);

			EditText_YS_LM_TOTALOUTPUT = (EditText) findViewById(R.id.editText_ys_lm_total_output);
			ys_lm_totaloutput = bundle.getStringExtra("YS_LM_TOTALOUTPUT");
			System.out.println(ys_lm_totaloutput);
			EditText_YS_LM_TOTALOUTPUT.setText(ys_lm_totaloutput);*/

			
		
}

	
	public boolean userLogin() {
		
		try {
			// On login click save related data in shared preference
			dialog = ProgressDialog.show(DashBoardActivity.this, "", "Loading Data...", true);			

			// Find the SharedPreferences value
			SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
			System.out.println("LOGINyyyyy DATA");
			String user_name = prefLoginReturn.getString("sUserName", "");
			System.out.println(user_name);
			String Pswd = prefLoginReturn.getString("sPassword", "");
			System.out.println(Pswd);
			String catg = prefLoginReturn.getString("sCategory", "");
			System.out.println(catg);

			JSONObject jsonObject = new JSONObject();
			jsonObject.put("sUserName", user_name);
			jsonObject.put("sPassword", Pswd);
			jsonObject.put("sCategory", catg);

			RequestParams param = new RequestParams();
			param.put("sJsonInput", jsonObject.toString());			
			RestService.post(METHOD_NAME, param, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);
					System.out.println("Testing3");
				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					String status;
					String message;
					try {
						// Find Login Response
						System.out.println("Testing2");
						System.out.println(jsonResponse);
						status = jsonResponse.getString("Status").toString();
						message = jsonResponse.getString("Message").toString();

						// Find Response for DashBoard
						tb_tm_new_value = jsonResponse.getString("TB_TM_NewCases_Per").toString();
						System.out.println("Testing");
						System.out.println(tb_tm_new_value);
						tb_tm_closed_value = jsonResponse.getString("TB_TM_ClosedCases_Per").toString();
						tb_lm_new_value = jsonResponse.getString("TB_LM_NewCases_Per").toString();
						tb_lm_closed_value = jsonResponse.getString("TB_LM_ClosedCases_Per").toString();
						
						ys_tm_turnaround = jsonResponse.getString("YS_TM_Turnaround").toString();
						ys_tm_totaloutput = jsonResponse.getString("YS_TM_Totaloutput").toString();
						ys_lm_totaloutput = jsonResponse.getString("YS_LM_Totaloutput").toString();

						tb_tm_NewCases = jsonResponse.getString("TB_TM_NewCases").toString();
						tb_tm_ClosedCases = jsonResponse.getString("TB_TM_ClosedCases").toString();
						tb_lm_NewCases = jsonResponse.getString("TB_LM_NewCases").toString();
						tb_lm_ClosedCases = jsonResponse.getString("TB_LM_ClosedCases").toString();
						
						ys_lm_turnaround = jsonResponse.getString("YS_LM_Turnaround").toString();
						
						
						priority_response = jsonResponse.getString("Priority").toString();
						action_response = jsonResponse.getString("Action").toString();
						open_response = jsonResponse.getString("Open").toString();
						
						dotask();
									
						

					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
										
						dialog.dismiss();
						return;					
					
				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					System.out.println(arg0);
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);
					System.out.println("Testing4");
					return null;
				}
			});

		} catch (Exception e) {
			System.out.println(e.toString());
		}
		return true;
	}

	
	
}