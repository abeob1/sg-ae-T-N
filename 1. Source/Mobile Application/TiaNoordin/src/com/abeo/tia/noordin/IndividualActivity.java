package com.abeo.tia.noordin;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.annotation.SuppressLint;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

@SuppressLint("Recycle")
public class IndividualActivity extends BaseActivity {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_ListofPropertyEnquiry

	// Find dropDown for gender and addressToUse web method
	private final String METHOD_DROPDOWN = "SPA_GetValidValues";
	// Find ADD INDIVIDUAL web method
	private final String METHOD_ADD_INDIVIDUAL = "SPA_AddIndividual";
	// Find EDIT INDIVIDUAL web method
	private final String METHOD_EDIT_INDIVIDUAL = "SPA_EditIndividual";
	// Find FIND INDIVIDUAL related case web method
	private final String METHOD_RELATEDCASE_CORPORATE = "SPA_RelatedCases";

	// Find Navigation title
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	// Spinner element
	Spinner spinnerTitle, spinnerGender, spinnerAddressToUSe;

	// Find Button
	Button buttonIndividualWalkIn, buttonIndividualFind, buttonIndividualAdd, buttonIndividualEdit,
			buttonIndividualConfirm, buttonIndividualRelatedCases;

	// Find Edit Text field
	EditText individualFullName, individualIdNo1, individualIdNo3, individualTax, individualMobile, individualTelephone,
			individualOffice, individualIdAddress1, individualIdAddress2, individualIdAddress3, individualIdAddress4,
			individualIdAddress5, individualCrpAddress1, individualCrpAddress2, individualCrpAddress3,
			individualCrpAddress4, individualCrpAddress5, individualLastUpdate;

	SimpleAdapter sAdap;

	// Find String passing in Individaul UI Response
	String codeResponse, docEntryResponse, employeeNameResponse = "", titleResponse = "", genderResponse = "",
			iDNo1Response = "", iDNo3Response = "", taxNoResponse = "", mobileNoResponse = "", telephoneResponse = "",
			officeNoResponse = "", iDAddress1Response = "", iDAddress2Response = "", iDAddress3Response = "",
			iDAddress4Response = "", iDAddress5Response = "", corresAddr1Response = "", corresAddr2Response = "",
			corresAddr3Response = "", corresAddr4Response = "", corresAddr5Response = "", addressToUseResponse = "",
			lastUpdatedOnResponse = "";

	// Find JSON Array
	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;
	ArrayList<HashMap<String, String>> jsonCaselist;

	// Find String for corporate case list
	String caseFileNo = "", relatedFileNo = "", branchCode = "", fileOpenedDate = "", iC = "", caseType = "",
			clientName = "", bankName = "", branch = "", lOTNo = "", caseAmount = "", userCode = "", status = "",
			fileClosedDate = "";

	// Find boolean value for button
	Boolean isADD = false, isFind = false, isEdit = false;

	// Find Web Service Message
	String messageDisplay = "", StatusResult = "";
	// Find dropdpwn list
	ArrayList<HashMap<String, String>> jsonlistTitle, jsonlistGender, jsonlistAddress;
	String id, name;
	TextView textTitle, textGender, textAddress;
	String titleValue, genderValue, addressUseToValue;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_individual);

		// load titles from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);

		// Find Button by Id
		buttonIndividualWalkIn = (Button) findViewById(R.id.button_IndividualWalkin);
		buttonIndividualFind = (Button) findViewById(R.id.button_IndividualFind);
		buttonIndividualAdd = (Button) findViewById(R.id.button_IndividualAdd);
		buttonIndividualEdit = (Button) findViewById(R.id.button_IndividualEdit);
		buttonIndividualConfirm = (Button) findViewById(R.id.button_IndividualConfirm);
		buttonIndividualRelatedCases = (Button) findViewById(R.id.button_IndividualRelateCases);

		// Find Edit Text field by Id
		individualFullName = (EditText) findViewById(R.id.editText_IndividualFullName);
		individualIdNo1 = (EditText) findViewById(R.id.editText_IndividualIdNo1);
		individualIdNo3 = (EditText) findViewById(R.id.editText_IndividualIdNo3);
		individualTax = (EditText) findViewById(R.id.editText_IndividualTax);
		individualMobile = (EditText) findViewById(R.id.editText_IndividualMobile);
		individualTelephone = (EditText) findViewById(R.id.editText_IndividualTelephone);
		individualOffice = (EditText) findViewById(R.id.editText_IndividualOffice);
		individualIdAddress1 = (EditText) findViewById(R.id.editText_IndividualIdAddress1);
		individualIdAddress2 = (EditText) findViewById(R.id.editText_IndividualIdAddress2);
		individualIdAddress3 = (EditText) findViewById(R.id.editText_IndividualIdAddress3);
		individualIdAddress4 = (EditText) findViewById(R.id.editText_IndividualIdAddress4);
		individualIdAddress5 = (EditText) findViewById(R.id.editText_IndividualIdAddress5);
		individualCrpAddress1 = (EditText) findViewById(R.id.editText_IndividualCrpAddress1);
		individualCrpAddress2 = (EditText) findViewById(R.id.editText_IndividualCrpAddress2);
		individualCrpAddress3 = (EditText) findViewById(R.id.editText_IndividualCrpAddress3);
		individualCrpAddress4 = (EditText) findViewById(R.id.editText_IndividualCrpAddress4);
		individualCrpAddress5 = (EditText) findViewById(R.id.editText_IndividualCrpAddress5);
		individualLastUpdate = (EditText) findViewById(R.id.editText_IndividualLastUpdate);
		individualFullName.requestFocus();

		// Find By Id spinner Address To Use
		spinnerTitle = (Spinner) findViewById(R.id.spinner_IndividualTitle);
		spinnerGender = (Spinner) findViewById(R.id.spinner_IndividualGender);
		spinnerAddressToUSe = (Spinner) findViewById(R.id.spinner_IndividualAddressToUse);

		// Spinner Title click listener
		spinnerTitle.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
				textTitle = (TextView) view.findViewById(R.id.Id);
				titleValue = textTitle.getText().toString();

				// Showing selected spinner item
				Toast.makeText(parent.getContext(), "Selected: " + titleValue, Toast.LENGTH_LONG).show();

			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub

			}
		});

		// Spinner Gender click listener
		spinnerGender.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

				textGender = (TextView) view.findViewById(R.id.Id);
				genderValue = textGender.getText().toString();

				// Showing selected spinner item
				Toast.makeText(parent.getContext(), "Selected: " + genderValue, Toast.LENGTH_LONG).show();

			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub

			}
		});

		// Spinner click listener
		spinnerAddressToUSe.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
				textAddress = (TextView) view.findViewById(R.id.Id);
				addressUseToValue = textAddress.getText().toString();
				// Showing selected spinner item
				Toast.makeText(parent.getContext(), "Selected: " + addressUseToValue, Toast.LENGTH_LONG).show();

			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub

			}
		});

		// Find Edit field Enable
		individualFullName.setEnabled(true);
		individualFullName.setFocusableInTouchMode(true);
		individualFullName.requestFocus();

		spinnerTitle.setEnabled(true);
		spinnerTitle.setFocusableInTouchMode(true);

		spinnerGender.setEnabled(true);
		spinnerGender.setFocusableInTouchMode(true);

		individualIdNo1.setEnabled(true);
		individualIdNo1.setFocusableInTouchMode(true);

		individualIdNo3.setEnabled(true);
		individualIdNo3.setFocusableInTouchMode(true);

		individualTax.setEnabled(true);
		individualTax.setFocusableInTouchMode(true);

		individualMobile.setEnabled(true);
		individualMobile.setFocusableInTouchMode(true);

		individualTelephone.setEnabled(true);
		individualTelephone.setFocusableInTouchMode(true);

		individualOffice.setEnabled(true);
		individualOffice.setFocusableInTouchMode(true);

		individualIdAddress1.setEnabled(true);
		individualIdAddress1.setFocusableInTouchMode(true);

		individualIdAddress2.setEnabled(true);
		individualIdAddress2.setFocusableInTouchMode(true);

		individualIdAddress3.setEnabled(true);
		individualIdAddress3.setFocusableInTouchMode(true);

		individualIdAddress4.setEnabled(true);
		individualIdAddress4.setFocusableInTouchMode(true);

		individualIdAddress5.setEnabled(true);
		individualIdAddress5.setFocusableInTouchMode(true);

		individualCrpAddress1.setEnabled(true);
		individualCrpAddress1.setFocusableInTouchMode(true);

		individualCrpAddress2.setEnabled(true);
		individualCrpAddress2.setFocusableInTouchMode(true);

		individualCrpAddress3.setEnabled(true);
		individualCrpAddress3.setFocusableInTouchMode(true);

		individualCrpAddress4.setEnabled(true);
		individualCrpAddress4.setFocusableInTouchMode(true);

		individualCrpAddress5.setEnabled(true);
		individualCrpAddress5.setFocusableInTouchMode(true);

		spinnerAddressToUSe.setEnabled(true);
		spinnerAddressToUSe.setFocusableInTouchMode(true);

		individualLastUpdate.setEnabled(false);
		individualLastUpdate.setFocusableInTouchMode(false);

		// buttonIndividualWalkIn button enable
		buttonIndividualWalkIn.setEnabled(true);
		buttonIndividualWalkIn.setClickable(true);
		buttonIndividualWalkIn.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

		// buttonIndividualAdd button enable
		buttonIndividualAdd.setEnabled(true);
		buttonIndividualAdd.setClickable(true);
		buttonIndividualAdd.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

		// buttonIndividualConfirm button enable
		buttonIndividualConfirm.setEnabled(false);
		buttonIndividualConfirm.setClickable(false);
		buttonIndividualConfirm.setTextColor(getApplication().getResources().getColor(R.color.gray));

		// buttonIndividualRelatedCase button enable
		buttonIndividualRelatedCases.setEnabled(true);
		buttonIndividualRelatedCases.setClickable(true);
		buttonIndividualRelatedCases.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

		// Result Bundle getting from list item click to property activity
		Bundle b = getIntent().getExtras();
		if (b != null) {

			// buttonIndividualWalkIn button enable
			buttonIndividualWalkIn.setEnabled(true);
			buttonIndividualWalkIn.setClickable(true);
			buttonIndividualWalkIn.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

			// buttonIndividualAdd button enable
			buttonIndividualAdd.setEnabled(false);
			buttonIndividualAdd.setClickable(false);
			buttonIndividualAdd.setTextColor(getApplication().getResources().getColor(R.color.gray));

			// buttonIndividualEdit button enable
			buttonIndividualEdit.setEnabled(true);
			buttonIndividualEdit.setClickable(true);
			buttonIndividualEdit.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

			// Find Edit field disable
			individualFullName.setEnabled(false);
			individualFullName.setFocusable(false);
			individualFullName.requestFocus();

			spinnerTitle.setEnabled(false);
			spinnerTitle.setFocusable(false);

			spinnerGender.setEnabled(false);
			spinnerGender.setFocusable(false);

			individualIdNo1.setEnabled(false);
			individualIdNo1.setFocusable(false);
			individualIdNo1.setText("");

			individualIdNo3.setEnabled(false);
			individualIdNo3.setFocusable(false);
			individualIdNo3.setText("");

			individualTax.setEnabled(false);
			individualTax.setFocusable(false);
			individualTax.setText("");

			individualMobile.setEnabled(false);
			individualMobile.setFocusable(false);
			individualMobile.setText("");

			individualTelephone.setEnabled(false);
			individualTelephone.setFocusable(false);
			individualTelephone.setText("");

			individualOffice.setEnabled(false);
			individualOffice.setFocusable(false);
			individualOffice.setText("");

			individualIdAddress1.setEnabled(false);
			individualIdAddress1.setFocusable(false);
			individualIdAddress1.setText("");

			individualIdAddress2.setEnabled(false);
			individualIdAddress2.setFocusable(false);
			individualIdAddress2.setText("");

			individualIdAddress3.setEnabled(false);
			individualIdAddress3.setFocusable(false);
			individualIdAddress3.setText("");

			individualIdAddress4.setEnabled(false);
			individualIdAddress4.setFocusable(false);
			individualIdAddress4.setText("");

			individualIdAddress5.setEnabled(false);
			individualIdAddress5.setFocusable(false);
			individualIdAddress5.setText("");

			individualCrpAddress1.setEnabled(false);
			individualCrpAddress1.setFocusable(false);
			individualCrpAddress1.setText("");

			individualCrpAddress2.setEnabled(false);
			individualCrpAddress2.setFocusable(false);
			individualCrpAddress2.setText("");

			individualCrpAddress3.setEnabled(false);
			individualCrpAddress3.setFocusable(false);
			individualCrpAddress3.setText("");

			individualCrpAddress4.setEnabled(false);
			individualCrpAddress4.setFocusable(false);
			individualCrpAddress4.setText("");

			individualCrpAddress5.setEnabled(false);
			individualCrpAddress5.setFocusable(false);
			individualCrpAddress5.setText("");

			spinnerAddressToUSe.setEnabled(false);
			spinnerAddressToUSe.setFocusable(false);

			individualLastUpdate.setEnabled(false);
			individualLastUpdate.setFocusable(false);
			individualLastUpdate.setText("");

			// Find Response
			codeResponse = b.getString("Code_T");
			System.out.println("Individual Search Response Text");
			System.out.println(codeResponse);

			docEntryResponse = b.getString("DocEntry_T");
			System.out.println(docEntryResponse);

			employeeNameResponse = b.getString("EmployeeName_T");
			System.out.println(employeeNameResponse);
			individualFullName.setText(employeeNameResponse);

			titleResponse = b.getString("Title_T");
			System.out.println(titleResponse);

			genderResponse = b.getString("Gender_T");
			System.out.println(genderResponse);

			iDNo1Response = b.getString("IDNo1_T");
			System.out.println(iDNo1Response);
			individualIdNo1.setText(iDNo1Response);

			iDNo3Response = b.getString("IDNo3_T");
			System.out.println(iDNo3Response);
			individualIdNo3.setText(iDNo3Response);

			taxNoResponse = b.getString("TaxNo_T");
			System.out.println(taxNoResponse);
			individualTax.setText(taxNoResponse);

			mobileNoResponse = b.getString("MobileNo_T");
			System.out.println(mobileNoResponse);
			individualMobile.setText(mobileNoResponse);

			telephoneResponse = b.getString("Telephone_T");
			System.out.println(telephoneResponse);
			individualTelephone.setText(telephoneResponse);

			officeNoResponse = b.getString("OfficeNo_T");
			System.out.println(officeNoResponse);
			individualOffice.setText(officeNoResponse);

			iDAddress1Response = b.getString("IDAddress1_T");
			System.out.println(iDAddress1Response);
			individualIdAddress1.setText(iDAddress1Response);

			iDAddress2Response = b.getString("IDAddress2_T");
			System.out.println(iDAddress2Response);
			individualIdAddress2.setText(iDAddress2Response);

			iDAddress3Response = b.getString("IDAddress3_T");
			System.out.println(iDAddress3Response);
			individualIdAddress3.setText(iDAddress3Response);

			iDAddress4Response = b.getString("IDAddress4_T");
			System.out.println(iDAddress4Response);
			individualIdAddress4.setText(iDAddress4Response);

			iDAddress5Response = b.getString("IDAddress5_T");
			System.out.println(iDAddress5Response);
			individualIdAddress5.setText(iDAddress5Response);

			corresAddr1Response = b.getString("CorresAddr1_T");
			System.out.println(corresAddr1Response);
			individualCrpAddress1.setText(corresAddr1Response);

			corresAddr2Response = b.getString("CorresAddr2_T");
			System.out.println(corresAddr2Response);
			individualCrpAddress2.setText(corresAddr2Response);

			corresAddr3Response = b.getString("CorresAddr3_T");
			System.out.println(corresAddr3Response);
			individualCrpAddress3.setText(corresAddr3Response);

			corresAddr4Response = b.getString("CorresAddr4_T");
			System.out.println(corresAddr4Response);
			individualCrpAddress4.setText(corresAddr4Response);

			corresAddr5Response = b.getString("CorresAddr5_T");
			System.out.println(corresAddr5Response);
			individualCrpAddress5.setText(corresAddr5Response);

			addressToUseResponse = b.getString("AddressToUse_T");
			System.out.println(addressToUseResponse);

			lastUpdatedOnResponse = b.getString("LastUpdatedOn_T");
			System.out.println(lastUpdatedOnResponse);
			individualLastUpdate.setText(lastUpdatedOnResponse);

		}

		// Find on Walkin button click Listener
		buttonIndividualWalkIn.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// Call intent to go walkin UI
				Intent intentWalkin = new Intent(IndividualActivity.this, WalkInActivity.class);
				startActivity(intentWalkin);

			}
		});

		// Find related case button click Listener
		buttonIndividualRelatedCases.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// Find web service for related case
				individualRelatedCaseList();

			}
		});

		// Find on edit button click Listener
		buttonIndividualAdd.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				isADD = true;
				
				// buttonIndividualAdd button enable
				buttonIndividualAdd.setEnabled(false);
				buttonIndividualAdd.setClickable(false);
				buttonIndividualAdd.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// buttonIndividualConfirm button enable
				buttonIndividualConfirm.setEnabled(true);
				buttonIndividualConfirm.setClickable(true);
				buttonIndividualConfirm.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));
				
			}
		});

		// Find on edit button click Listener
		buttonIndividualEdit.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// change boolean flag
				isEdit = true;

				// buttonIndividualWalkIn button enable
				buttonIndividualWalkIn.setEnabled(true);
				buttonIndividualWalkIn.setClickable(true);
				buttonIndividualWalkIn.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

				// buttonIndividualEdit button enable
				buttonIndividualEdit.setEnabled(false);
				buttonIndividualEdit.setClickable(false);
				buttonIndividualEdit.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// buttonIndividualConfirm button enable
				buttonIndividualConfirm.setEnabled(true);
				buttonIndividualConfirm.setClickable(true);
				buttonIndividualConfirm.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

				// Find Edit field Enable
				individualFullName.setEnabled(true);
				individualFullName.setFocusableInTouchMode(true);
				individualFullName.requestFocus();

				spinnerTitle.setEnabled(true);
				spinnerTitle.setFocusableInTouchMode(true);

				spinnerGender.setEnabled(true);
				spinnerGender.setFocusableInTouchMode(true);

				individualIdNo1.setEnabled(true);
				individualIdNo1.setFocusableInTouchMode(true);

				individualIdNo3.setEnabled(true);
				individualIdNo3.setFocusableInTouchMode(true);

				individualTax.setEnabled(true);
				individualTax.setFocusableInTouchMode(true);

				individualMobile.setEnabled(true);
				individualMobile.setFocusableInTouchMode(true);

				individualTelephone.setEnabled(true);
				individualTelephone.setFocusableInTouchMode(true);

				individualOffice.setEnabled(true);
				individualOffice.setFocusableInTouchMode(true);

				individualIdAddress1.setEnabled(true);
				individualIdAddress1.setFocusableInTouchMode(true);

				individualIdAddress2.setEnabled(true);
				individualIdAddress2.setFocusableInTouchMode(true);

				individualIdAddress3.setEnabled(true);
				individualIdAddress3.setFocusableInTouchMode(true);

				individualIdAddress4.setEnabled(true);
				individualIdAddress4.setFocusableInTouchMode(true);

				individualIdAddress5.setEnabled(true);
				individualIdAddress5.setFocusableInTouchMode(true);

				individualCrpAddress1.setEnabled(true);
				individualCrpAddress1.setFocusableInTouchMode(true);

				individualCrpAddress2.setEnabled(true);
				individualCrpAddress2.setFocusableInTouchMode(true);

				individualCrpAddress3.setEnabled(true);
				individualCrpAddress3.setFocusableInTouchMode(true);

				individualCrpAddress4.setEnabled(true);
				individualCrpAddress4.setFocusableInTouchMode(true);

				individualCrpAddress5.setEnabled(true);
				individualCrpAddress5.setFocusableInTouchMode(true);

				spinnerAddressToUSe.setEnabled(true);
				spinnerAddressToUSe.setFocusableInTouchMode(true);

				individualLastUpdate.setEnabled(false);
				individualLastUpdate.setFocusableInTouchMode(false);

			}
		});

		// Find on confirm button click listener
		buttonIndividualConfirm.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(IndividualActivity.this);

				// set title
				alertDialogBuilder.setTitle("Confirm");

				// set dialog message
				alertDialogBuilder.setMessage("Click yes to save!").setCancelable(false)
						.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int id) {

						if (isADD == true && isFind == false && isEdit == false) {
							// call edit webservice for individual add details
							addDataIndividualDetails();

						} else if (isADD == false && isFind == false && isEdit == true) {
							// call edit webservice for individual edit details
							editDataIndividualDetails();
						} else {

							// call edit webservice for individual edit details
							// findDataIndividualDetails();

						}

					}

				}).setNegativeButton("No", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int id) {
						// if this button is clicked, just close
						// the dialog box and do nothing
						dialog.cancel();
					}
				});
				// create alert dialog
				AlertDialog alertDialog = alertDialogBuilder.create();

				// show it
				alertDialog.show();

			}
		});
		// Call dropDown Function web service
		dropTitleDownGenderAddressUseTo();
	}

	public void individualRelatedCaseList() {
		try {

			// Passing value in JSON format in first 8-fields
			JSONObject jsonObject = new JSONObject();

			jsonObject.put("PropertyCode", "");
			jsonObject.put("RelatedPartyCode", codeResponse);
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
					Toast.makeText(IndividualActivity.this, "Case Item Found", Toast.LENGTH_SHORT).show();
					Intent intentList = new Intent(IndividualActivity.this, PropertyRelatedCaseListActivity.class);
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

	public void dropTitleDownGenderAddressUseTo() {
		// Passing value in JSON format in first fields

		/*
		 * SPA_GetValidValues()
		 * 
		 * The Input you have to pass for the Gender is : { "TableName":
		 * "@AE_RELATEDPARTY", "FieldName": "GENDER" }
		 * 
		 * The Input you have pass for the Address to Use is : { "TableName":
		 * "@AE_RELATEDPARTY", "FieldName": "ADDRESS_TOUSE" }
		 */
		JSONObject jsonObject = new JSONObject();

		try {

			String fieldName = "";
			RequestParams params = null;
			for (int i = 1; i <= 3; i++) {
				System.out.println(i);
				if (i == 1) {
					fieldName = "INDIVIDUAL_TITLE";
					jsonObject.put("TableName", "@AE_RELATEDPARTY");
					jsonObject.put("FieldName", fieldName);
					System.out.println(fieldName);
					params = new RequestParams();
					params.put("sJsonInput", jsonObject.toString());
					System.out.println(params);
					RestService.post(METHOD_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

						@Override
						public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
							// TODO Auto-generated method stub
							System.out.println(arg3);

						}

						@Override
						public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
							// TODO Auto-generated method stub
							System.out.println("IndividualActivity Title Dropdown Success Details ");
							System.out.println(arg2);

							try {

								arrayResponse = new JSONArray(arg2);
								// Create new list
								jsonlistTitle = new ArrayList<HashMap<String, String>>();

								for (int i = 0; i < arrayResponse.length(); i++) {

									jsonResponse = arrayResponse.getJSONObject(i);

									id = jsonResponse.getString("Id").toString();
									name = jsonResponse.getString("Name").toString();

									// SEND JSON DATA INTO SPINNER TITLE LIST
									HashMap<String, String> genderList = new HashMap<String, String>();

									// Send JSON Data to list activity
									System.out.println("SEND JSON TITLE LIST");
									genderList.put("Id_T", id);
									System.out.println(name);
									genderList.put("Name_T", name);
									System.out.println(name);
									System.out.println(" END SEND JSON TITLE LIST");

									jsonlistTitle.add(genderList);
									System.out.println("JSON TITLE LIST");
									System.out.println(jsonlistTitle);
								}
								// Spinner set Array Data in Drop down

								sAdap = new SimpleAdapter(IndividualActivity.this, jsonlistTitle, R.layout.spinner_item,
										new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

								spinnerTitle.setAdapter(sAdap);

								/*
								 * for (int i = 0; i <
								 * jsonlistTitle.get(0).keySet(); i++) { if
								 * (jsonlistTitle.get(0).get("Name_T").toString(
								 * ).equals(titleResponse)) {
								 * spinnerTitle.setSelection(i); break; } }
								 */ // int count=0;
								for (int j = 0; j < jsonlistTitle.size(); j++) {
									if (jsonlistTitle.get(j).get("Name_T").equals(titleResponse)) {
										spinnerTitle.setSelection(j);
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

							System.out.println("IndividualActivity Title Dropdown Details parse Response");
							System.out.println(arg0);
							return null;
						}
					});

				} else if (i == 2) {
					fieldName = "GENDER";
					System.out.println(fieldName);
					jsonObject.put("TableName", "@AE_RELATEDPARTY");
					jsonObject.put("FieldName", fieldName);
					params = new RequestParams();
					params.put("sJsonInput", jsonObject.toString());
					System.out.println(params);
					RestService.post(METHOD_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

						@Override
						public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
							// TODO Auto-generated method stub
							System.out.println(arg3);

						}

						@Override
						public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
							// TODO Auto-generated method stub
							System.out.println("IndividualActivity Gender Dropdown onSuccess");
							System.out.println(arg2);

							try {

								arrayResponse = new JSONArray(arg2);
								// Create new list
								jsonlistGender = new ArrayList<HashMap<String, String>>();

								for (int i = 0; i < arrayResponse.length(); i++) {
									jsonResponse = arrayResponse.getJSONObject(i);

									id = jsonResponse.getString("Id").toString();
									name = jsonResponse.getString("Name").toString();

									// SEND JSON DATA INTO SPINNER GENDER LIST
									HashMap<String, String> genderList = new HashMap<String, String>();

									// Send JSON Data to list activity
									System.out.println("SEND JSON Gender LIST");
									genderList.put("Id_T", id);
									System.out.println(name);
									genderList.put("Name_T", name);
									System.out.println(name);
									System.out.println(" END SEND JSON Gender LIST");

									jsonlistGender.add(genderList);
									System.out.println("JSON GENDERLIST");
									System.out.println(jsonlistGender);
								}
								// Spinner set Array Data in Drop down

								sAdap = new SimpleAdapter(IndividualActivity.this, jsonlistGender,
										R.layout.spinner_item, new String[] { "Id_T", "Name_T" },
										new int[] { R.id.Id, R.id.Name });

								spinnerGender.setAdapter(sAdap);

								// populating the bundle response in the spinner
								// set the value
								for (int j = 0; j < jsonlistGender.size(); j++) {
									if (jsonlistGender.get(j).get("Name_T").equals(genderResponse)) {
										spinnerGender.setSelection(j);
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

							System.out.println("IndividualActivity Gender Dropdown Details parseResponse");
							System.out.println(arg0);
							return null;
						}
					});

				} else if (i == 3) {
					fieldName = "ADDRESS_TOUSE";
					System.out.println(fieldName);
					jsonObject.put("TableName", "@AE_RELATEDPARTY");
					jsonObject.put("FieldName", fieldName);
					params = new RequestParams();
					params.put("sJsonInput", jsonObject.toString());
					System.out.println(params);
					RestService.post(METHOD_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

						@Override
						public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
							// TODO Auto-generated method stub
							System.out.println(arg3);

						}

						@Override
						public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
							// TODO Auto-generated method stub
							System.out.println("IndividualActivity ADDRESS_TOUSE Dropdown onSuccess");
							System.out.println(arg2);

							try {

								arrayResponse = new JSONArray(arg2);
								// Create new list
								jsonlistAddress = new ArrayList<HashMap<String, String>>();

								for (int i = 0; i < arrayResponse.length(); i++) {
									jsonResponse = arrayResponse.getJSONObject(i);

									id = jsonResponse.getString("Id").toString();
									name = jsonResponse.getString("Name").toString();

									// SEND JSON DATA INTO SPINNER GENDER LIST
									HashMap<String, String> addList = new HashMap<String, String>();

									// Send JSON Data to list activity
									System.out.println("SEND JSON ADDRESS_TOUSE LIST");
									addList.put("Id_T", id);
									System.out.println(name);
									addList.put("Name_T", name);
									System.out.println(name);
									System.out.println(" END SEND JSON ADDRESS_TOUSE LIST");

									jsonlistAddress.add(addList);
									System.out.println("JSON ADDRESS_TOUSE LIST");
									System.out.println(jsonlistAddress);
								}
								// Spinner set Array Data in Drop down

								sAdap = new SimpleAdapter(IndividualActivity.this, jsonlistAddress,
										R.layout.spinner_item, new String[] { "Id_T", "Name_T" },
										new int[] { R.id.Id, R.id.Name });

								spinnerAddressToUSe.setAdapter(sAdap);

								for (int j = 0; j < jsonlistAddress.size(); j++) {
									if (jsonlistAddress.get(j).get("Id_T").equals(addressToUseResponse)) {
										spinnerAddressToUSe.setSelection(j);
										break;
									}
								}

								// spinnerGender.setAdapter(sAdap);

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

							System.out.println("IndividualActivity ADDRESS_TOUSE Dropdown Details parseResponse");
							System.out.println(arg0);
							return null;
						}
					});

				}
			}

		} catch (

		JSONException e)

		{
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	// Find addDataIndividualDetails function
	public void addDataIndividualDetails() {

		Toast.makeText(IndividualActivity.this, "Add individual", Toast.LENGTH_SHORT).show();
		/*
		 * { "Code": "", "DocEntry": "", "EmployeeName": "Vino", "Title": "MR",
		 * "Gender": "LELAKI", "IDNo1": "484424-08-5248", "IDNo3": "7584298",
		 * "TaxNo": "5524", "MobileNo": "9876545489", "Telephone": "",
		 * "OfficeNo": "", "IDAddress1": "47", "IDAddress2": "SAM",
		 * "IDAddress3": "kn road", "IDAddress4": "chennai", "IDAddress5":
		 * "tamilnadu", "CorresAddr1": "44", "CorresAddr2": "ram nagar",
		 * "CorresAddr3": "ECR tail", "CorresAddr4": "chennai", "CorresAddr5":
		 * "tamilnadu", "AddressToUse": "ADDRESS_ID", "LastUpdatedOn": "" }
		 */
		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		try {

			jsonObject.put("EmployeeName", individualFullName.getText().toString());
			jsonObject.put("Title", titleValue);
			jsonObject.put("Gender", genderValue);
			jsonObject.put("IDNo1", individualIdNo1.getText().toString());
			jsonObject.put("IDNo3", individualIdNo3.getText().toString());
			jsonObject.put("TaxNo", individualTax.getText().toString());
			jsonObject.put("MobileNo", individualMobile.getText().toString());
			jsonObject.put("Telephone", individualTelephone.getText().toString());
			jsonObject.put("OfficeNo", individualOffice.getText().toString());
			jsonObject.put("IDAddress1", individualIdAddress1.getText().toString());
			jsonObject.put("IDAddress2", individualIdAddress2.getText().toString());
			jsonObject.put("IDAddress3", individualIdAddress3.getText().toString());
			jsonObject.put("IDAddress4", individualIdAddress4.getText().toString());
			jsonObject.put("IDAddress5", individualIdAddress5.getText().toString());
			jsonObject.put("CorresAddr1", individualCrpAddress1.getText().toString());
			jsonObject.put("CorresAddr2", individualCrpAddress2.getText().toString());
			jsonObject.put("CorresAddr3", individualCrpAddress3.getText().toString());
			jsonObject.put("CorresAddr4", individualCrpAddress4.getText().toString());
			jsonObject.put("CorresAddr5", individualCrpAddress5.getText().toString());
			jsonObject.put("AddressToUse", addressUseToValue);
			jsonObject.put("LastUpdatedOn", individualLastUpdate.getText().toString());

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_ADD_INDIVIDUAL, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("IndividualActivity Details Add Confirmed");
					System.out.println(arg2);

					// Find status Response
					try {
						StatusResult = jsonResponse.getString("Result").toString();
						System.out.println(StatusResult);
						messageDisplay = jsonResponse.getString("DisplayMessage").toString();
						System.out.println(messageDisplay);
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					if (StatusResult.equals("Success")) {
						Intent idividualBack = new Intent(IndividualActivity.this, IndividualActivity.class);
						startActivity(idividualBack);

						Toast.makeText(IndividualActivity.this, messageDisplay, Toast.LENGTH_SHORT).show();
					} else {
						Toast.makeText(IndividualActivity.this, messageDisplay, Toast.LENGTH_SHORT).show();

					}
				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println("IndividualActivity Details Add Response");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public void editDataIndividualDetails() {
		Toast.makeText(IndividualActivity.this, "Edit individual", Toast.LENGTH_SHORT).show();
		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		try {
			/*
			 * { "Code": "000000000015", "DocEntry": "15", "EmployeeName":
			 * "Naveen", "Title": "MR", "Gender": "LELAKI", "IDNo1":
			 * "484424-08-5193", "IDNo3": "7584239", "TaxNo": "5541",
			 * "MobileNo": "7667266203", "Telephone": "", "OfficeNo": "",
			 * "IDAddress1": "47", "IDAddress2": "raja st1", "IDAddress3":
			 * "kavin road1", "IDAddress4": "chennai", "IDAddress5":
			 * "tamilnadu", "CorresAddr1": "44", "CorresAddr2": "ram nagar1",
			 * "CorresAddr3": "ECR tail1", "CorresAddr4": "chennai",
			 * "CorresAddr5": "tamilnadu", "AddressToUse": "ADDRESS_ID",
			 * "LastUpdatedOn": "" }
			 */
			jsonObject.put("Code", codeResponse);
			jsonObject.put("DocEntry", docEntryResponse);
			jsonObject.put("EmployeeName", individualFullName.getText().toString());
			jsonObject.put("Title", titleValue);
			jsonObject.put("Gender", genderValue);
			jsonObject.put("IDNo1", individualIdNo1.getText().toString());
			jsonObject.put("IDNo3", individualIdNo3.getText().toString());
			jsonObject.put("TaxNo", individualTax.getText().toString());
			jsonObject.put("MobileNo", individualMobile.getText().toString());
			jsonObject.put("Telephone", individualTelephone.getText().toString());
			jsonObject.put("OfficeNo", individualOffice.getText().toString());
			jsonObject.put("IDAddress1", individualIdAddress1.getText().toString());
			jsonObject.put("IDAddress2", individualIdAddress2.getText().toString());
			jsonObject.put("IDAddress3", individualIdAddress3.getText().toString());
			jsonObject.put("IDAddress4", individualIdAddress4.getText().toString());
			jsonObject.put("IDAddress5", individualIdAddress5.getText().toString());
			jsonObject.put("CorresAddr1", individualCrpAddress1.getText().toString());
			jsonObject.put("CorresAddr2", individualCrpAddress2.getText().toString());
			jsonObject.put("CorresAddr3", individualCrpAddress3.getText().toString());
			jsonObject.put("CorresAddr4", individualCrpAddress4.getText().toString());
			jsonObject.put("CorresAddr5", individualCrpAddress5.getText().toString());
			jsonObject.put("AddressToUse", addressUseToValue);
			jsonObject.put("LastUpdatedOn", individualLastUpdate.getText().toString());

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_EDIT_INDIVIDUAL, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Edit individual Details Confirmed");
					System.out.println(arg2);

					// Find status Response
					try {
						StatusResult = jsonResponse.getString("Result").toString();
						messageDisplay = jsonResponse.getString("DisplayMessage").toString();
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					if (StatusResult.equals("Success")) {
						Intent iAddBack = new Intent(context, IndividualActivity.class);
						startActivity(iAddBack);

						Toast.makeText(IndividualActivity.this, messageDisplay, Toast.LENGTH_SHORT).show();
					} else {
						Toast.makeText(IndividualActivity.this, messageDisplay, Toast.LENGTH_SHORT).show();

					}
				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println("Edit individual Details Parse Response");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

}
