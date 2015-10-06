package com.abeo.tia.noordin;


import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.app.Activity;
import android.content.Intent;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class WalkInActivity extends BaseActivity {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx/SPA_IndividualSearch

	// Find list of Individual enquiry list web method
	private final String METHOD_SEARCH_INDIVIDUAL = "SPA_IndividualSearch";

	// Find list of Corporate enquiry list web method
	private final String METHOD_SEARCH_CORPORATE = "SPA_CorporateSearch";

	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	// Find Button
	Button buttonIndividualSearch, buttonCorporateSearch;
	public String tag = "";

	// Find Edit Text field
	EditText walkInIndividualFullName, walkInIndividualMobileNo, walkInIndividualIDNo;
	EditText walkIncorporateCoName, walkIncorporateRegNo, walkIncorporateAddress;

	// Find String passing in Individaul UI
	String code, docEntry, employeeName = "", title = "", gender = "", iDNo1 = "", iDNo3 = "", taxNo = "",
			mobileNo = "", telephone = "", officeNo = "", iDAddress1 = "", iDAddress2 = "", iDAddress3 = "",
			iDAddress4 = "", iDAddress5 = "", corresAddr1 = "", corresAddr2 = "", corresAddr3 = "", corresAddr4 = "",
			corresAddr5 = "", addressToUse = "", lastUpdatedOn = "";

	// Find String passing in corporate UI
	String codeC = "", docEntryC = "", compNameC = "", bRNNoC = "", taxNoC = "", OfficeNoC = "", iDAddress1C = "",
			iDAddress2C = "", iDAddress3C = "", iDAddress4C = "", iDAddress5C = "", corresAddr1C = "",
			corresAddr2C = "", corresAddr3C = "", corresAddr4C = "", corresAddr5C = "", addressToUseC = "",
			lastUpdatedOnC = "", dirCode1C = "", dirName1C = "", dirContactNum1C = "", dirName2C = "",
			dirContactNum2C = "", dirName3C = "", dirContactNum3C = "", dirName4C = "", dirContactNum4C = "",
			dirName5C = "", dirContactNum5C = "";

	// Find JSON Array
	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_walkin);
		// load titles from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		// Find Set Function
		set(navMenuTitles, navMenuIcons);
		// Find Edit by Id
		walkInIndividualFullName = (EditText) findViewById(R.id.editText_WalkInIndividualFullName);
		walkInIndividualMobileNo = (EditText) findViewById(R.id.editText_WalkInIndividualMobileNo);
		walkInIndividualIDNo = (EditText) findViewById(R.id.editText_WalkInIndividualIDNo);
		// Find Edit for corporate
		walkIncorporateCoName = (EditText) findViewById(R.id.editText_WalkinCorporateCoName);
		walkIncorporateRegNo = (EditText) findViewById(R.id.editText_WalkinCorporateRegNo);
		walkIncorporateAddress = (EditText) findViewById(R.id.editText_WalkinCorporateAddress);

		// Find button by Id
		buttonIndividualSearch = (Button) findViewById(R.id.Button_WalkinSearchIndividual);
		buttonCorporateSearch = (Button) findViewById(R.id.Button_WalkinSearchCorporate);

		buttonIndividualSearch.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				Toast.makeText(WalkInActivity.this, "Walk-in individual Search button click", Toast.LENGTH_LONG).show();
				Log.e(tag, "Walk-in individual search Button Clicked");

				// call edit webservice for property details
				searchIndividualDataDetails();

			}
		});
		buttonCorporateSearch.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				Toast.makeText(WalkInActivity.this, " Corporate Search button click", Toast.LENGTH_LONG).show();
				Log.e(tag, "Walk-in Corporate search Button Clicked");
				// Find the Search corporate Function
				searchCorporateDataDetails();

			}
		});

	}

	public void searchCorporateDataDetails() {

		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		try {

			String comName = walkIncorporateCoName.getText().toString();
			String regNum = walkIncorporateRegNo.getText().toString();
			String addr1 = walkIncorporateAddress.getText().toString();

			if (comName.equals("") || regNum.equals("") && addr1.equals("")) {
				if (comName.equals("")) {
					Toast.makeText(WalkInActivity.this, "Fill Company Name:XXXX", Toast.LENGTH_SHORT).show();
				}
				if (regNum.equals("")) {
					Toast.makeText(WalkInActivity.this, "Fill BRN NO:XXXX-A", Toast.LENGTH_SHORT).show();
				}
				if (addr1.equals("")) {
					Toast.makeText(WalkInActivity.this, "Fill Address1", Toast.LENGTH_SHORT).show();
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
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						try {
							
							
							 
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

						
						
						 
						// Find Intent to call view property details
						Intent i = new Intent(WalkInActivity.this, CorporateActivity.class);

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
					protected String parseResponse(String arg0, boolean arg1)
							throws Throwable {
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
				/*RestService.post(METHOD_SEARCH_CORPORATE, params, new BaseJsonHttpResponseHandler<String>() {

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
						 
						// Find Intent to call view property details
						Intent i = new Intent(WalkInActivity.this, CorporateActivity.class);

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
				});*/
			}
		} catch (

		JSONException e)

		{
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public void searchIndividualDataDetails() {
		Toast.makeText(WalkInActivity.this, "Search individual", Toast.LENGTH_SHORT).show();

		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		try {

			String fullName = walkInIndividualFullName.getText().toString();
			String mobNum = walkInIndividualMobileNo.getText().toString();
			String idNum = walkInIndividualIDNo.getText().toString();

			if (fullName.equals("") || mobNum.equals("") && idNum.equals("")) {
				if (fullName.equals("")) {
					Toast.makeText(WalkInActivity.this, "Fill Full Name", Toast.LENGTH_SHORT).show();
				}
				if (mobNum.equals("")) {
					Toast.makeText(WalkInActivity.this, "Fill Mobile No", Toast.LENGTH_SHORT).show();
				}
				if (idNum.equals("")) {
					Toast.makeText(WalkInActivity.this, "Fill Id No", Toast.LENGTH_SHORT).show();
				}
			} else {

				// {
				// "FullName": "CHAI SEEN",
				// "MobileNum": "9715450006",
				// "IDNum": "481024-08-5193",
				// "Category": "SPA"
				// }
				jsonObject.put("FullName", fullName);
				jsonObject.put("MobileNum", mobNum);
				jsonObject.put("IDNum", idNum);
				jsonObject.put("Category", "SPA");

				RequestParams params = new RequestParams();
				params.put("sJsonInput", jsonObject.toString());
				System.out.println(params);

				RestService.post(METHOD_SEARCH_INDIVIDUAL, params, new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);

					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
						// TODO Auto-generated method stub
						System.out.println("WalkInActivity Details Add Confirmed");
						System.out.println(arg2);
						Intent iWalkInActivity = new Intent(WalkInActivity.this, WalkInActivity.class);
						startActivity(iWalkInActivity);
						//

						try {
							code = jsonResponse.getString("Code").toString();
							docEntry = jsonResponse.getString("DocEntry").toString();
							employeeName = jsonResponse.getString("EmployeeName").toString();
							title = jsonResponse.getString("Title").toString();
							gender = jsonResponse.getString("Gender").toString();
							iDNo1 = jsonResponse.getString("IDNo1").toString();
							iDNo3 = jsonResponse.getString("IDNo3").toString();
							taxNo = jsonResponse.getString("TaxNo").toString();
							mobileNo = jsonResponse.getString("MobileNo").toString();
							telephone = jsonResponse.getString("Telephone").toString();
							officeNo = jsonResponse.getString("OfficeNo").toString();
							iDAddress1 = jsonResponse.getString("IDAddress1").toString();
							iDAddress2 = jsonResponse.getString("IDAddress2").toString();
							iDAddress3 = jsonResponse.getString("IDAddress3").toString();
							iDAddress4 = jsonResponse.getString("IDAddress4").toString();
							iDAddress5 = jsonResponse.getString("IDAddress5").toString();
							corresAddr1 = jsonResponse.getString("CorresAddr1").toString();
							corresAddr2 = jsonResponse.getString("CorresAddr2").toString();
							corresAddr3 = jsonResponse.getString("CorresAddr3").toString();
							corresAddr4 = jsonResponse.getString("CorresAddr4").toString();
							corresAddr5 = jsonResponse.getString("CorresAddr5").toString();
							addressToUse = jsonResponse.getString("AddressToUse").toString();
							lastUpdatedOn = jsonResponse.getString("LastUpdatedOn").toString();

						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}

						/*
						 * { EmployeeName: "CHAI SEEN", Title: "", Gender:
						 * "LELAKI", IDNo1: "481024-08-5193", IDNo3: "7588439",
						 * TaxNo: "12345", MobileNo: "9715450006", Telephone:
						 * "", OfficeNo: "", IDAddress1: "NO 29", IDAddress2:
						 * "JALAN BENTARA DALAM", IDAddress3: "TAMAN ISKANDAR",
						 * IDAddress4: "80050 JOHOR BAHRU", IDAddress5: "JOHOR",
						 * CorresAddr1: "", CorresAddr2: "", CorresAddr3: "",
						 * CorresAddr4: "", CorresAddr5: "", AddressToUse:
						 * "ADDRESS_ID", LastUpdatedOn: "02/09/2015" }
						 */
						// Find Intent to call view property details
						Intent i = new Intent(WalkInActivity.this, IndividualActivity.class);
						Toast.makeText(WalkInActivity.this, "Item Clicked", Toast.LENGTH_SHORT).show();

						// Send the property details in property UI through
						// intent

						System.out.println("Walk-in search send value");

						i.putExtra("Code_T", code);
						System.out.println(code);
						i.putExtra("DocEntry_T", docEntry);
						System.out.println(docEntry);
						i.putExtra("EmployeeName_T", employeeName);
						System.out.println(employeeName);
						i.putExtra("Title_T", title);
						System.out.println(title);
						i.putExtra("Gender_T", gender);
						System.out.println(gender);
						i.putExtra("IDNo1_T", iDNo1);
						System.out.println(iDNo1);
						i.putExtra("IDNo3_T", iDNo3);
						System.out.println(iDNo3);
						i.putExtra("TaxNo_T", taxNo);
						System.out.println(taxNo);
						i.putExtra("MobileNo_T", mobileNo);
						System.out.println(mobileNo);
						i.putExtra("Telephone_T", telephone);
						System.out.println(telephone);
						i.putExtra("OfficeNo_T", officeNo);
						System.out.println(officeNo);
						i.putExtra("IDAddress1_T", iDAddress1);
						System.out.println(iDAddress1);
						i.putExtra("IDAddress2_T", iDAddress2);
						System.out.println(iDAddress2);
						i.putExtra("IDAddress3_T", iDAddress3);
						System.out.println(iDAddress3);
						i.putExtra("IDAddress4_T", iDAddress4);
						System.out.println(iDAddress4);
						i.putExtra("IDAddress5_T", iDAddress5);
						System.out.println(iDAddress5);
						i.putExtra("CorresAddr1_T", corresAddr1);
						System.out.println(corresAddr1);
						i.putExtra("CorresAddr2_T", corresAddr2);
						System.out.println(corresAddr2);
						i.putExtra("CorresAddr3_T", corresAddr3);
						System.out.println(corresAddr3);
						i.putExtra("CorresAddr4_T", corresAddr4);
						System.out.println(corresAddr4);
						i.putExtra("CorresAddr5_T", corresAddr5);
						System.out.println(corresAddr5);
						i.putExtra("AddressToUse_T", addressToUse);
						System.out.println(addressToUse);
						i.putExtra("LastUpdatedOn_T", lastUpdatedOn);
						System.out.println(lastUpdatedOn);

						startActivity(i);

					}

					@Override
					protected String parseResponse(String arg0, boolean arg1) throws Throwable {

						// Get Json response
						arrayResponse = new JSONArray(arg0);
						jsonResponse = arrayResponse.getJSONObject(0);

						System.out.println("WalkInActivity Details ParseResponse");
						System.out.println(arg0);
						return null;
					}
				});
			}
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}
}