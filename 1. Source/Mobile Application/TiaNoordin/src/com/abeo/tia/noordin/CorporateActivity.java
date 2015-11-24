package com.abeo.tia.noordin;

import java.util.ArrayList;
import java.util.HashMap;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemSelectedListener;

public class CorporateActivity extends BaseActivity {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_ListofPropertyEnquiry

	// Find ADD corporate web method
	private final String METHOD_ADD_CORPORATE = "";
	// Find corporate search web method
	private final String METHOD_SEARCH_CORPORATE = "SPA_CorporateSearch";
	// Find corporate related case web method
	private final String METHOD_RELATEDCASE_CORPORATE = "SPA_RelatedCases";

	// Find Navigation title
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	// Find Button
	Button buttonCorporateWalkIn, buttonCorporateSearch, buttonCorporateRelatedCase, buttonCorporateEdit,
			buttonCorporateConfirm;

	// Find Edit Text field
	EditText corporateCoName, corporateBrnNo, corporateTaxNo, corporateOffice, corporateAddress1, corporateAddress2,
			corporateAddress3, corporateAddress4, corporateAddress5, corporateCrpAddress1, corporateCrpAddress2,
			corporateCrpAddress3, corporateCrpAddress4, corporateCrpAddress5, corporateDirector1, corporateDirContact1,
			corporateDirector2, corporateDirContact2, corporateDirector3, corporateDirContact3, corporateDirector4,
			corporateDirContact4, corporateDirector5, corporateDirContact5, corporateAddressToUse, corporateLastUpdate;

	// Find String passing in corporate UI Response
	String codeCResponse = "", docEntryCResponse = "", compNameCResponse = "", bRNNoCResponse = "", taxNoCResponse = "",
			OfficeNoCResponse = "", iDAddress1CResponse = "", iDAddress2CResponse = "", iDAddress3CResponse = "",
			iDAddress4CResponse = "", iDAddress5CResponse = "", corresAddr1CResponse = "", corresAddr2CResponse = "",
			corresAddr3CResponse = "", corresAddr4CResponse = "", corresAddr5CResponse = "", addressToUseCResponse = "",
			lastUpdatedOnCResponse = "", dirCode1CResponse = "", dirName1CResponse = "", dirContactNum1CResponse = "";

	// Find String passing in corporate UI
	String codeC = "", docEntryC = "", compNameC = "", bRNNoC = "", taxNoC = "", OfficeNoC = "", iDAddress1C = "",
			iDAddress2C = "", iDAddress3C = "", iDAddress4C = "", iDAddress5C = "", corresAddr1C = "",
			corresAddr2C = "", corresAddr3C = "", corresAddr4C = "", corresAddr5C = "", addressToUseC = "",
			lastUpdatedOnC = "", dirCode1C = "", dirName1C = "", dirContactNum1C = "", dirName2C = "",
			dirContactNum2C = "", dirName3C = "", dirContactNum3C = "", dirName4C = "", dirContactNum4C = "",
			dirName5C = "", dirContactNum5C = "";

	// Find String for corporate case list
	String caseFileNo = "", relatedFileNo = "", branchCode = "", fileOpenedDate = "", iC = "", caseType = "",
			clientName = "", bankName = "", branch = "", lOTNo = "", caseAmount = "", userCode = "", status = "",
			fileClosedDate = "";

	
			
	// Find JSON Array
	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;
	ArrayList<HashMap<String, String>> jsonCaselist;

	// Find Web Service Message
	String messageDisplay = "", StatusResult = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_corporate);
		// load titles from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);

		// Find Button by Id
		buttonCorporateWalkIn = (Button) findViewById(R.id.button_CorporateWalkin);
		buttonCorporateSearch = (Button) findViewById(R.id.button_CorporateSearch);
		buttonCorporateRelatedCase = (Button) findViewById(R.id.button_CorporateRelateCases);
		buttonCorporateEdit = (Button) findViewById(R.id.button_CorporateEdit);
		buttonCorporateConfirm = (Button) findViewById(R.id.button_CorporateConfirm);
		// Find the SharedPreferences Firstname
					SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);		
					String FirName = FirstName.getString("FIRSETNAME", "");
					TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
					welcome.setText("Welcome "+FirName);
					
		// Find Edit Text field by Id

		corporateCoName = (EditText) findViewById(R.id.editText_CorporateCoName);
		corporateBrnNo = (EditText) findViewById(R.id.editText_CorporateBrnNo);
		corporateTaxNo = (EditText) findViewById(R.id.editText_CorporateTaxNo);
		corporateOffice = (EditText) findViewById(R.id.editText_CorporateOffice);
		corporateAddress1 = (EditText) findViewById(R.id.editText_CorporateAddress1);
		corporateAddress2 = (EditText) findViewById(R.id.editText_CorporateAddress2);
		corporateAddress3 = (EditText) findViewById(R.id.editText_CorporateAddress3);
		corporateAddress4 = (EditText) findViewById(R.id.editText_CorporateAddress4);
		corporateAddress5 = (EditText) findViewById(R.id.editText_CorporateAddress5);
		corporateCrpAddress1 = (EditText) findViewById(R.id.editText_CorporateCrpAddress1);
		corporateCrpAddress2 = (EditText) findViewById(R.id.editText_CorporateCrpAddress2);
		corporateCrpAddress3 = (EditText) findViewById(R.id.editText_CorporateCrpAddress3);
		corporateCrpAddress4 = (EditText) findViewById(R.id.editText_CorporateCrpAddress4);
		corporateCrpAddress5 = (EditText) findViewById(R.id.editText_CorporateCrpAddress5);
		corporateDirector1 = (EditText) findViewById(R.id.editText_CorporateDirector1);
		corporateDirContact1 = (EditText) findViewById(R.id.editText_CorporateDirContact1);
		corporateDirector2 = (EditText) findViewById(R.id.editText_CorporateDirector2);
		corporateDirContact2 = (EditText) findViewById(R.id.editText_CorporateDirContact2);
		corporateDirector3 = (EditText) findViewById(R.id.editText_CorporateDirector3);
		corporateDirContact3 = (EditText) findViewById(R.id.editText_CorporateDirContact3);
		corporateDirector4 = (EditText) findViewById(R.id.editText_CorporateDirector4);
		corporateDirContact4 = (EditText) findViewById(R.id.editText_CorporateDirContact4);
		corporateDirector5 = (EditText) findViewById(R.id.editText_CorporateDirector5);
		corporateDirContact5 = (EditText) findViewById(R.id.editText_CorporateDirContact5);
		corporateAddressToUse = (EditText) findViewById(R.id.editText_CorporateCrpAddressToUse);
		corporateLastUpdate = (EditText) findViewById(R.id.editText_CorporateLastupdate);

		// Find Edit Fields enable
		corporateCoName.setEnabled(true);
		corporateCoName.setFocusableInTouchMode(true);
		corporateCoName.requestFocus();

		corporateBrnNo.setEnabled(true);
		corporateBrnNo.setFocusableInTouchMode(true);

		corporateAddress1.setEnabled(true);
		corporateAddress1.setFocusableInTouchMode(true);

		// Result Bundle getting from list item click to property activity
		Bundle b = getIntent().getExtras();
		if (b != null) {
			// CorporateSearch button disable
			buttonCorporateSearch.setEnabled(false);
			buttonCorporateSearch.setClickable(false);
			buttonCorporateSearch.setTextColor(getApplication().getResources().getColor(R.color.gray));

			// Corporate Related case list button enable
			buttonCorporateRelatedCase.setEnabled(true);
			buttonCorporateRelatedCase.setClickable(true);
			buttonCorporateRelatedCase.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

			codeCResponse = b.getString("CodeC_T");
			System.out.println("Corporate Search Response Text");
			System.out.println(codeCResponse);
			docEntryCResponse = b.getString("DocEntryC_T");
			System.out.println(docEntryCResponse);

			compNameCResponse = b.getString("CompNameC_T");
			System.out.println(compNameCResponse);
			corporateCoName.setText(compNameCResponse);
			bRNNoCResponse = b.getString("BRNNoC_T");
			System.out.println(bRNNoCResponse);
			corporateBrnNo.setText(bRNNoCResponse);
			taxNoCResponse = b.getString("TaxNoC_T");
			System.out.println(taxNoCResponse);
			corporateTaxNo.setText(taxNoCResponse);
			OfficeNoCResponse = b.getString("OfficeNoC_T");
			System.out.println(OfficeNoCResponse);
			corporateOffice.setText(OfficeNoCResponse);

			iDAddress1CResponse = b.getString("IDAddress1C_T");
			System.out.println(iDAddress1CResponse);
			corporateAddress1.setText(iDAddress1CResponse);
			iDAddress2CResponse = b.getString("IDAddress2C_T");
			System.out.println(iDAddress2CResponse);
			corporateAddress2.setText(iDAddress2CResponse);
			iDAddress3CResponse = b.getString("IDAddress3C_T");
			System.out.println(iDAddress3CResponse);
			corporateAddress3.setText(iDAddress3CResponse);
			iDAddress4CResponse = b.getString("IDAddress4C_T");
			System.out.println(iDAddress4CResponse);
			corporateAddress4.setText(iDAddress4CResponse);
			iDAddress5CResponse = b.getString("IDAddress5C_T");
			System.out.println(iDAddress5CResponse);
			corporateAddress5.setText(iDAddress5CResponse);

			corresAddr1CResponse = b.getString("CorresAddr1C_T");
			System.out.println(corresAddr1CResponse);
			corporateCrpAddress1.setText(corresAddr1CResponse);
			corresAddr2CResponse = b.getString("CorresAddr2C_T");
			System.out.println(corresAddr2CResponse);
			corporateCrpAddress2.setText(corresAddr2CResponse);
			corresAddr3CResponse = b.getString("CorresAddr3C_T");
			System.out.println(corresAddr3CResponse);
			corporateCrpAddress3.setText(corresAddr3CResponse);
			corresAddr4CResponse = b.getString("CorresAddr4C_T");
			System.out.println(corresAddr4CResponse);
			corporateCrpAddress4.setText(corresAddr4CResponse);
			corresAddr5CResponse = b.getString("CorresAddr5C_T");
			System.out.println(corresAddr5CResponse);
			corporateCrpAddress5.setText(corresAddr5CResponse);

			addressToUseCResponse = b.getString("AddressToUseC_T");
			System.out.println(addressToUseCResponse);
			corporateAddressToUse.setText(addressToUseCResponse);
			lastUpdatedOnCResponse = b.getString("LastUpdatedOnC_T");
			System.out.println(lastUpdatedOnCResponse);
			corporateLastUpdate.setText(lastUpdatedOnCResponse);

			dirName1CResponse = b.getString("DirName1C_T");
			System.out.println(dirName1CResponse);
			corporateDirector1.setText(dirName1CResponse);
			dirContactNum1CResponse = b.getString("DirContactNum1C_T");
			System.out.println(dirContactNum1CResponse);
			corporateDirContact1.setText(dirContactNum1CResponse);
		}

		// Find on Walkin button click Listener
		buttonCorporateWalkIn.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// Call intent to go walkin UI
				Intent intentWalkin = new Intent(CorporateActivity.this, WalkInActivity.class);
				startActivity(intentWalkin);

			}
		});

		// Find on find button click Listener
		buttonCorporateSearch.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// Find the Search corporate Function
				searchCorporateDataDetails();

			}
		});
		buttonCorporateRelatedCase.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				corporateRelatedCaseList();
			}
		});

	}

	protected void corporateRelatedCaseList() {
		/*
		 * { "PropertyCode": "", "RelatedPartyCode": "000000000002", "CallFrom":
		 * "RELATEDPARTY", "Category": "SPA" }
		 */
		try {

			// Passing value in JSON format in first 8-fields
			JSONObject jsonObject = new JSONObject();

			jsonObject.put("PropertyCode", "");
			jsonObject.put("RelatedPartyCode", codeCResponse);
			jsonObject.put("CallFrom", "RELATEDPARTY");
			jsonObject.put("Category", "SPA");

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_RELATEDCASE_CORPORATE, params, new BaseJsonHttpResponseHandler<String>() {

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
					//Toast.makeText(CorporateActivity.this, "Case Item Found", Toast.LENGTH_SHORT).show();
					Intent intentList = new Intent(CorporateActivity.this, PropertyRelatedCaseListActivity.class);
					intentList.putExtra("ProjectJsonList", jsonCaselist);
					startActivity(intentList);
					System.out.println(arg2);

				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					System.out.println("Property Case list OnParseResponse");
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

	public void searchCorporateDataDetails() {

		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		try {
			String comName = corporateCoName.getText().toString();
			String regNum = corporateBrnNo.getText().toString();
			String addr1 = corporateAddress1.getText().toString();

			if (regNum.equals("") && (comName.equals("") || addr1.equals(""))) {
				if (comName.equals("")) {
					Toast.makeText(CorporateActivity.this, "Fill Company Name:XXXX", Toast.LENGTH_SHORT).show();
				}
				else if (regNum.equals("")) {
					Toast.makeText(CorporateActivity.this, "Fill BRN NO:XXXX-A", Toast.LENGTH_SHORT).show();
				}
				else if (addr1.equals("")) {
					Toast.makeText(CorporateActivity.this, "Fill Address1", Toast.LENGTH_SHORT).show();
				}
			} else {

				/*
				 * { "CompanyName": "SERI", "RegNum": "41628-A", "Address": "",
				 * "Category": "SPA" }
				 */
				jsonObject.put("CompanyName", comName);
				jsonObject.put("RegNum", regNum);
				jsonObject.put("Address", addr1);
				jsonObject.put("Category", "SPA");

				RequestParams params = new RequestParams();
				params.put("sJsonInput", jsonObject.toString());
				System.out.println(params);

				RestService.post(METHOD_SEARCH_CORPORATE, params, new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);

					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
						// TODO Auto-generated method stub
						System.out.println("WalkInActivity Corporate onSucessConfirmed");
						System.out.println(arg2);

						//

						try {
							/*
							 * { Code: "000000000002", DocEntry: "2", CompName:
							 * "SERI ALAM PROPERTIES SDN. BHD.", BRNNo:
							 * "41628-A", TaxNo: "", OfficeNo: "41628-A",
							 * IDAddress1: "No:8", IDAddress2: "Jalan suria",
							 * IDAddress3: "Bandar Seri Alam" , IDAddress4:
							 * "81750 Masai", IDAddress5: "", CorresAddr1: "",
							 * CorresAddr2: "", CorresAddr3: "", CorresAddr4:
							 * "", CorresAddr5: "", AddressToUse:
							 * "ADDRESS_CORRESPOND", LastUpdatedOn:
							 * "03/09/2015", Director: [ { DirCode:
							 * "000000000001", DirName: "CHAI SEEN",
							 * DirContactNum: "9715450006" } ] }
							 */
							codeC = jsonResponse.getString("Code").toString();
							docEntryC = jsonResponse.getString("DocEntry").toString();

							compNameC = jsonResponse.getString("CompName").toString();
							bRNNoC = jsonResponse.getString("BRNNo").toString();
							taxNoC = jsonResponse.getString("TaxNo").toString();
							OfficeNoC = jsonResponse.getString("OfficeNo").toString();

							iDAddress1C = jsonResponse.getString("IDAddress1").toString();
							iDAddress2C = jsonResponse.getString("IDAddress2").toString();
							iDAddress3C = jsonResponse.getString("IDAddress3").toString();
							iDAddress4C = jsonResponse.getString("IDAddress4").toString();
							iDAddress5C = jsonResponse.getString("IDAddress5").toString();

							corresAddr1C = jsonResponse.getString("CorresAddr1").toString();
							corresAddr2C = jsonResponse.getString("CorresAddr2").toString();
							corresAddr3C = jsonResponse.getString("CorresAddr3").toString();
							corresAddr4C = jsonResponse.getString("CorresAddr4").toString();
							corresAddr5C = jsonResponse.getString("CorresAddr5").toString();

							addressToUseC = jsonResponse.getString("AddressToUse").toString();
							lastUpdatedOnC = jsonResponse.getString("LastUpdatedOn").toString();

							JSONArray jsonDir = jsonResponse.getJSONArray("Director");
							for (int j = 0; j < jsonDir.length(); j++) {
								JSONObject dir = jsonDir.getJSONObject(j);

								System.out.println("Director Details");
								dirName1C = dir.getString("DirName").toString();
								System.out.println(dirName1C);
								dirContactNum1C = dir.getString("DirContactNum").toString();
								System.out.println(dirContactNum1C);
							}

						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}

						/*
						 * { Code: "000000000002", DocEntry: "2", CompName:
						 * "SERI ALAM PROPERTIES SDN. BHD.", BRNNo: "41628-A",
						 * TaxNo: "", OfficeNo: "41628-A", IDAddress1: "No:8",
						 * IDAddress2: "Jalan suria", IDAddress3:
						 * "Bandar Seri Alam" , IDAddress4: "81750 Masai",
						 * IDAddress5: "", CorresAddr1: "", CorresAddr2: "",
						 * CorresAddr3: "", CorresAddr4: "", CorresAddr5: "",
						 * AddressToUse: "ADDRESS_CORRESPOND", LastUpdatedOn:
						 * "03/09/2015", Director: [ { DirCode: "000000000001",
						 * DirName: "CHAI SEEN", DirContactNum: "9715450006" } ]
						 * }
						 */
						// Find Intent to call view property details
						Intent i = new Intent(CorporateActivity.this, CorporateActivity.class);

						// Send the property details in property UI through
						// intent

						System.out.println("Walk-in Corporate search Send value");

						i.putExtra("CodeC_T", codeC);
						System.out.println(codeC);
						i.putExtra("DocEntryC_T", docEntryC);
						System.out.println(docEntryC);

						i.putExtra("CompNameC_T", compNameC);
						System.out.println(compNameC);
						i.putExtra("BRNNoC_T", bRNNoC);
						System.out.println(bRNNoC);
						i.putExtra("TaxNoC_T", taxNoC);
						System.out.println(taxNoC);
						i.putExtra("OfficeNoC_T", OfficeNoC);
						System.out.println(OfficeNoC);

						i.putExtra("IDAddress1C_T", iDAddress1C);
						System.out.println(iDAddress1C);
						i.putExtra("IDAddress2C_T", iDAddress2C);
						System.out.println(iDAddress2C);
						i.putExtra("IDAddress3C_T", iDAddress3C);
						System.out.println(iDAddress3C);
						i.putExtra("IDAddress4C_T", iDAddress4C);
						System.out.println(iDAddress4C);
						i.putExtra("IDAddress5C_T", iDAddress5C);
						System.out.println(iDAddress5C);

						i.putExtra("CorresAddr1C_T", corresAddr1C);
						System.out.println(corresAddr1C);
						i.putExtra("CorresAddr2C_T", corresAddr2C);
						System.out.println(corresAddr2C);
						i.putExtra("CorresAddr3C_T", corresAddr3C);
						System.out.println(corresAddr3C);
						i.putExtra("CorresAddr4C_T", corresAddr4C);
						System.out.println(corresAddr4C);
						i.putExtra("CorresAddr5C_T", corresAddr5C);
						System.out.println(corresAddr5C);

						i.putExtra("AddressToUseC_T", addressToUseC);
						System.out.println(addressToUseC);
						i.putExtra("LastUpdatedOnC_T", lastUpdatedOnC);
						System.out.println(lastUpdatedOnC);

						i.putExtra("DirName1C_T", dirName1C);
						System.out.println(dirName1C);
						i.putExtra("DirContactNum1C_T", dirContactNum1C);
						System.out.println(dirContactNum1C);
						i.putExtra("DirName2C_T", dirName2C);
						System.out.println(dirName2C);
						i.putExtra("DirContactNum2C_T", dirContactNum2C);
						System.out.println(dirContactNum2C);
						i.putExtra("DirName3C_T", dirName3C);
						System.out.println(dirName3C);
						i.putExtra("DirContactNum4C_T", dirContactNum4C);
						System.out.println(dirContactNum4C);
						i.putExtra("DirName5C_T", dirName5C);
						System.out.println(dirName5C);
						i.putExtra("DirContactNum5C_T", dirContactNum5C);
						System.out.println(dirContactNum5C);

						startActivity(i);

					}

					@Override
					protected String parseResponse(String arg0, boolean arg1) throws Throwable {

						// Get Json response
						arrayResponse = new JSONArray(arg0);
						for (int j = 0; j < arrayResponse.length(); j++) {
							jsonResponse = arrayResponse.getJSONObject(j);
						}

						System.out.println("WalkInActivity Corporate Details ParseResponse");
						System.out.println(arg0);
						return null;
					}
				});
			}
		} catch (

		JSONException e)

		{
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}
	

	public boolean dispatchTouchEvent(MotionEvent ev) {	       
        InputMethodManager imm = (InputMethodManager)getSystemService(Context.
                INPUT_METHOD_SERVICE);
imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
        return super.dispatchTouchEvent(ev);

        } 

}
