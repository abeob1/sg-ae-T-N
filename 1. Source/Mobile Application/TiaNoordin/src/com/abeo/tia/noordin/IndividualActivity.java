package com.abeo.tia.noordin;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ZoomButton;

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
	
	private static final String METHOD_ADDCASE_DOCUMENT = "SPA_AddCase_ScanIC";

	// Find Navigation title
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	private File selectedFile;
	public String fileName;
	ProgressDialog dialog = null;
	
	String itemCode = "", itemName = "";
	
	/********** File Path *************/
	int serverResponseCode = 0;
	String upLoadServerUri = null;
	String selectedImagePath;
	final String uploadFilePath = Environment.getExternalStorageDirectory().getPath();
	final String uploadFileName = "/go.png";
	
	ZoomButton btnpdf1, btnpdf2;
	
	String CARPORINDU = "INDIVIDUAL",FILEUPLOADRESULT = "",FILEUPLOADRESULT2="",pdflink1="",pdflink2="";

	// Spinner element
	Spinner spinnerTitle, spinnerGender, spinnerAddressToUSe;

	// Find Button
	Button buttonIndividualWalkIn, buttonIndividualFind, buttonIndividualAdd, buttonIndividualEdit,
			buttonIndividualConfirm, buttonIndividualRelatedCases,btnFontIc,btnBackIc;

	// Find Edit Text field
	EditText individualFullName, individualIdNo1, individualIdNo3, individualTax, individualMobile, individualTelephone,
			individualOffice, individualIdAddress1, individualIdAddress2, individualIdAddress3, individualIdAddress4,
			individualIdAddress5, individualCrpAddress1, individualCrpAddress2, individualCrpAddress3,
			individualCrpAddress4, individualCrpAddress5, individualLastUpdate;

	SimpleAdapter sAdap;
	
	private static final int REQUEST_PICK_FILE = 1;
	private static final int REQUEST_PICK_FILE2 = 2;

	private final String METHOD_ADDFILE = "http://54.251.51.69:3878/SPAMobile.asmx/Attachments";
	
	
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
		
		btnpdf1 = (ZoomButton) findViewById(R.id.frntic);
		btnpdf2 = (ZoomButton) findViewById(R.id.backic);
		
		// Find the SharedPreferences Firstname
					SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);		
					String FirName = FirstName.getString("FIRSETNAME", "");
					TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
					welcome.setText("Welcome "+FirName);

		// Find Button by Id
		buttonIndividualWalkIn = (Button) findViewById(R.id.button_IndividualWalkin);
		buttonIndividualFind = (Button) findViewById(R.id.button_IndividualFind);
		buttonIndividualAdd = (Button) findViewById(R.id.button_IndividualAdd);
		buttonIndividualEdit = (Button) findViewById(R.id.button_IndividualEdit);
		buttonIndividualConfirm = (Button) findViewById(R.id.button_IndividualConfirm);
		buttonIndividualRelatedCases = (Button) findViewById(R.id.button_IndividualRelateCases);
		
		btnFontIc = (Button) findViewById(R.id.button_AddCaseStep3FontIc);
		btnBackIc = (Button) findViewById(R.id.button_AddCaseStep3BackIc);

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
				//Toast.makeText(parent.getContext(), "Selected: " + titleValue, Toast.LENGTH_LONG).show();

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
				//Toast.makeText(parent.getContext(), "Selected: " + genderValue, Toast.LENGTH_LONG).show();

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
				//Toast.makeText(parent.getContext(), "Selected: " + addressUseToValue, Toast.LENGTH_LONG).show();

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
			
			FILEUPLOADRESULT= b.getString("FrontIC_T");
			 FILEUPLOADRESULT2 = b.getString("BackIC_T");
			 
			
			if(!FILEUPLOADRESULT.isEmpty())
			{
				System.out.println("DDDDD");
				 System.out.println(FILEUPLOADRESULT);
				btnpdf1.setClickable(true);
				btnpdf1.setEnabled(true);
			}
			else
			{
				btnpdf1.setClickable(false);
				btnpdf1.setEnabled(false);
			}
			
			if(!FILEUPLOADRESULT2.isEmpty())
			{
				btnpdf2.setClickable(true);				
				btnpdf2.setEnabled(true);
			}
			else
			{
				btnpdf2.setClickable(false);				
				btnpdf2.setEnabled(false);
			}
			
			
			
			
			
			

		}
		// Find on Front Ic  button click Listener
		btnFontIc.setOnClickListener(new OnClickListener() {

					@Override
					public void onClick(View v) {
						//initiatePopupWindow();
						System.out.println("Clicked btnFontIc");
						Intent intent = new Intent(IndividualActivity.this, FilePicker.class);
						startActivityForResult(intent, REQUEST_PICK_FILE);

					}
				});
				
		
		// Find on Back Ic button click Listener
		btnpdf1.setOnClickListener(new OnClickListener() {

					@Override
					public void onClick(View v) {
						//initiatePopupWindow();
						System.out.println("Clicked btnBackIc");
						 if(!FILEUPLOADRESULT.isEmpty())
						   {
							 String googleDocsUrl;
							 String filenameArray[] = FILEUPLOADRESULT.split("\\.");
						        String extension = filenameArray[filenameArray.length-1];
								if(extension.equals("pdf"))
									googleDocsUrl = "http://docs.google.com/viewer?url=http://54.251.51.69:3878"+ FILEUPLOADRESULT;
								else			 
									googleDocsUrl = "http://54.251.51.69:3878"+FILEUPLOADRESULT;
						   
						   Intent intent = new Intent(Intent.ACTION_VIEW);
						   intent.setDataAndType(Uri.parse(googleDocsUrl ), "text/html");
						   startActivity(intent);
						   }
						 else
						 {
							 slog("No Files Avilable to Display.");
						 }

					}
				});
		
		// Find on Back Ic button click Listener
		btnpdf2.setOnClickListener(new OnClickListener() {

					@Override
					public void onClick(View v) {
						//initiatePopupWindow();
						System.out.println("Clicked btnBackIc");			
						if(!FILEUPLOADRESULT2.isEmpty())
						   {
							String googleDocsUrl;
							 String filenameArray[] = FILEUPLOADRESULT2.split("\\.");
						        String extension = filenameArray[filenameArray.length-1];
								if(extension.equals("pdf"))
									googleDocsUrl = "http://docs.google.com/viewer?url=http://54.251.51.69:3878"+ FILEUPLOADRESULT2;
								else			 
									googleDocsUrl = "http://54.251.51.69:3878"+FILEUPLOADRESULT2;
						   
						   Intent intent = new Intent(Intent.ACTION_VIEW);
						   intent.setDataAndType(Uri.parse(googleDocsUrl ), "text/html");
						   startActivity(intent);
						   }
						else
						 {
							 slog("No Files Avilable to Display.");
						 }

					}
				});
				// Find on Back Ic button click Listener
		btnBackIc.setOnClickListener(new OnClickListener() {

					@Override
					public void onClick(View v) {
						//initiatePopupWindow();
						System.out.println("Clicked btnBackIc");
						Intent intent = new Intent(IndividualActivity.this, FilePicker.class);
						startActivityForResult(intent, REQUEST_PICK_FILE2);

					}
				});

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
				
				btnBackIc.setClickable(true);
				btnFontIc.setClickable(true);
				btnpdf1.setClickable(true);
				btnpdf2.setClickable(true);
				btnpdf1.setEnabled(true);
				btnpdf2.setEnabled(true);
				
				
				
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
		
		btnBackIc.setClickable(false);
		btnFontIc.setClickable(false);
		//btnpdf1.setClickable(false);
		//btnpdf2.setClickable(false);
		//btnpdf1.setEnabled(false);
		//btnpdf2.setEnabled(false);

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
					//Toast.makeText(IndividualActivity.this, "Case Item Found", Toast.LENGTH_SHORT).show();
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

		//Toast.makeText(IndividualActivity.this, "Add individual", Toast.LENGTH_SHORT).show();
		// Find the SharedPreferences pass Login value
				SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
				System.out.println("LOGIN DATA");
				String userName = prefLoginReturn.getString("sUserName", "");
				
				String category = prefLoginReturn.getString("sCategory", "");
				System.out.println(category);
				String CardCode = prefLoginReturn.getString("CardCode", "");
				System.out.println(CardCode);
				
				
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

			jsonObject.put("CardCode", CardCode);
			jsonObject.put("Code", codeResponse);
			jsonObject.put("DocEntry", "");
			jsonObject.put("IDType", CARPORINDU);
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
			
			jsonObject.put("ScanFrontICLocation", pdflink1);
			
			jsonObject.put("ScanBackICLocation", pdflink2);

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
		//Toast.makeText(IndividualActivity.this, "Edit individual", Toast.LENGTH_SHORT).show();
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
	
	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data) {

		if (resultCode == RESULT_OK) {

			switch (requestCode) {

			case REQUEST_PICK_FILE:

				if (data.hasExtra(FilePicker.EXTRA_FILE_PATH)) {

					selectedFile = new File(data.getStringExtra(FilePicker.EXTRA_FILE_PATH));
					fileName = selectedFile.getPath();
					dialog = ProgressDialog.show(IndividualActivity.this, "", "Uploading file...", true);
					 new Thread(new Runnable() {
		                    public void run() {              
		                    	uploadFile(fileName);
		                    	
		                    	
		                    }
		                }).start();
					
				}
				break;
			case REQUEST_PICK_FILE2:

				if (data.hasExtra(FilePicker.EXTRA_FILE_PATH)) {

					selectedFile = new File(data.getStringExtra(FilePicker.EXTRA_FILE_PATH));
					fileName = selectedFile.getPath();
					dialog = ProgressDialog.show(IndividualActivity.this, "", "Uploading file...", true);
					 new Thread(new Runnable() {
		                    public void run() {              
		                    	uploadFile2(fileName);
		                    	
		                    	
		                    }
		                }).start();
				}
				break;
			}
		}
	}
	
	

	public void addCaseDocumentToRead2() {
		
		// Find the SharedPreferences pass Login value
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
		System.out.println("LOGIN DATA");
		String userName = prefLoginReturn.getString("sUserName", "");
		
		String category = prefLoginReturn.getString("sCategory", "");
		System.out.println(category);
		String CardCode = prefLoginReturn.getString("CardCode", "");
		System.out.println(CardCode);
		
		/*
		 * { "ItemCode": "200046", "ItemName": "ORIGINAL PROPERTY TITLE",
		 * "FileName": "", "DocBinaryArray": "", "CardCode": "1500000134" }
		 */
		// File from File Path
		String file = fileName.substring(fileName.lastIndexOf('/') + 1);
		System.out.println(file);
		System.out.println("ok add case");

		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		// jsonObject.put("Category", "SPA");
		try {
			

			// Find the SharedPreferences value
			SharedPreferences ItemData = getSharedPreferences("ItemData", Context.MODE_PRIVATE);
			System.out.println("ItemData");
			String ItemCode = ItemData.getString("ItemCode", "");
			System.out.println(ItemCode);
			String ItemName = ItemData.getString("ItemName", "");
			System.out.println(ItemName);
			
			
			
			System.out.println("Param Inputs");
			jsonObject.put("ItemCode", ItemCode);
			System.out.println(itemCode);
			jsonObject.put("ItemName", ItemName);
			System.out.println(itemName);
			jsonObject.put("FileName", FILEUPLOADRESULT2);
			System.out.println(fileName);
			//jsonObject.put("sDoc", "");
			//System.out.println(sb);
			jsonObject.put("ICType", "Back");
			System.out.println(CardCode);

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println("params");
			System.out.println(params);

			RestService.post(METHOD_ADDCASE_DOCUMENT, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);
					dialog.dismiss();

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Document Send  Confirmed");
					System.out.println(arg2);
					try {
						JSONArray arry = new JSONArray(arg2.toString());				
						jsonResponse = arry.getJSONObject(0);
						//setavalubyRC(jsonResponse);
					} catch (JSONException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}
					
					

					String StatusResult = null;
					String messageDisplay = null;
					// Find status Response
					try {
						//StatusResult = jsonResponse.getString("Result").toString();
						messageDisplay = jsonResponse.getString("Message").toString();
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					

						
						dialog.dismiss();
						try {
							setallvalues2(jsonResponse);
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
						
					
				}

				private void setFIC2(String fileName) {
					// TODO Auto-generated method stub
					//ImageView imageView=(ImageView) findViewById(R.id.ImageView_AddCaseStep3BackIc);
					//Picasso.with(context).load("http://54.251.51.69:3878/FileUpload/"+FILEUPLOADRESULT).into(imageView);
					
				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println("Document Details ParseResponse");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}


	public void addCaseDocumentToRead() {
		
		// Find the SharedPreferences pass Login value
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
		System.out.println("LOGIN DATA");
		String userName = prefLoginReturn.getString("sUserName", "");
		
		String category = prefLoginReturn.getString("sCategory", "");
		System.out.println(category);
		String CardCode = prefLoginReturn.getString("CardCode", "");
		System.out.println(CardCode);
		
		/*
		 * { "ItemCode": "200046", "ItemName": "ORIGINAL PROPERTY TITLE",
		 * "FileName": "", "DocBinaryArray": "", "CardCode": "1500000134" }
		 */
		// File from File Path
		String file = fileName.substring(fileName.lastIndexOf('/') + 1);
		System.out.println(file);
		System.out.println("ok add case");

		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		// jsonObject.put("Category", "SPA");
		try {
			
			// Find confirmation message
			// messageResult = jsonObject.getString("Result").toString();
			
			// Find the SharedPreferences value
			SharedPreferences ItemData = getSharedPreferences("ItemData", Context.MODE_PRIVATE);
			System.out.println("ItemData");
			String ItemCode = ItemData.getString("ItemCode", "");
			System.out.println(ItemCode);
			String ItemName = ItemData.getString("ItemName", "");
			System.out.println(ItemName);
			
			
			
			System.out.println("Param Inputs");
			jsonObject.put("ItemCode", ItemCode);
			System.out.println(itemCode);
			jsonObject.put("ItemName", ItemName);
			System.out.println(itemName);
			jsonObject.put("FileName", FILEUPLOADRESULT);
			System.out.println(fileName);
			//jsonObject.put("sDoc", "");
			//System.out.println(sb);
			jsonObject.put("ICType", "Front");
			//System.out.println(CardCode);

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println("params");
			System.out.println(params);

			RestService.post(METHOD_ADDCASE_DOCUMENT, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);
					dialog.dismiss();

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Document Send  Confirmed");
					System.out.println(arg2);
					
					try {
						JSONArray arry = new JSONArray(arg2.toString());				
						jsonResponse = arry.getJSONObject(0);
						setavalubyRC(jsonResponse);
					} catch (JSONException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}

					String StatusResult = null;
					String messageDisplay = null;
					// Find status Response
					try {
						//StatusResult = jsonResponse.getString("Result").toString();
						messageDisplay = jsonResponse.getString("Message").toString();
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					
						 dialog.dismiss();
							
						
							

					
					
				}

				
				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println("Document Details ParseResponse");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	

	public void setavalubyRC(Object data) throws JSONException {
		
	

		//Toast.makeText(IndividualActivity.this, "You Clicked at " + data,
			//	Toast.LENGTH_LONG).show();
		JSONObject jObj =  new JSONObject(data.toString());
		//JSONObject jObj = arr.getJSONObject(0);
		System.out.println(jObj);
		
		
		
		individualFullName.setText(jObj.getString("EmployeeName"));
		individualIdNo1.setText(jObj.getString("IDNo1"));
		individualIdNo3.setText(jObj.getString("IDNo3"));
		individualTax.setText(jObj.getString("TaxNo"));
		individualMobile.setText(jObj.getString("MobileNo"));
		individualTelephone.setText(jObj.getString("Telephone"));
		individualOffice.setText(jObj.getString("OfficeNo"));
		
		individualIdAddress1.setText(jObj.getString("IDAddress1"));
		individualIdAddress2.setText(jObj.getString("IDAddress2"));
		individualIdAddress3.setText(jObj.getString("IDAddress3"));
		individualIdAddress4.setText(jObj.getString("IDAddress4"));
		individualIdAddress5.setText(jObj.getString("IDAddress5"));
		
		individualCrpAddress1.setText(jObj.getString("CorresAddr1"));
		individualCrpAddress2.setText(jObj.getString("CorresAddr2"));
		individualCrpAddress3.setText(jObj.getString("CorresAddr3"));
		individualCrpAddress4.setText(jObj.getString("CorresAddr4"));
		individualCrpAddress5.setText(jObj.getString("CorresAddr5"));

		individualFullName.requestFocus();
		
		pdflink1= jObj.getString("ScanFrontICLocation");
		
		codeResponse = jObj.getString("Code");
		
		//spinnerpropertyTitleType.setEnabled(false);
		//spinnerpropertyGENDER.setEnabled(false);
		//spinnerpropertyaddressToUse.setEnabled(false);
		
	}
	
	
	
	public int uploadFile2(String sourceFileUri) {

        String fileName = sourceFileUri;

        HttpURLConnection conn = null;
        DataOutputStream dos = null;  
        String lineEnd = "\r\n";
        String twoHyphens = "--";
        String boundary = "*****";
        int bytesRead, bytesAvailable, bufferSize;
        byte[] buffer;
        int maxBufferSize = 1 * 1024 * 1024; 
        File sourceFile = new File(sourceFileUri); 

        if (!sourceFile.isFile()) {
            dialog.dismiss(); 
            Log.e("uploadFile", "Source File not exist :" +fileName);
            return 0;
        }
        else
        {
            try { 

                // open a URL connection to the Servlet
                FileInputStream fileInputStream = new FileInputStream(sourceFile);
                URL url = new URL(METHOD_ADDFILE);

                // Open a HTTP  connection to  the URL
                conn = (HttpURLConnection) url.openConnection(); 
                conn.setDoInput(true); // Allow Inputs
                conn.setDoOutput(true); // Allow Outputs
                conn.setUseCaches(false); // Don't use a Cached Copy
                conn.setRequestMethod("POST");
                conn.setRequestProperty("Connection", "Keep-Alive");
                conn.setRequestProperty("ENCTYPE", "multipart/form-data");
                conn.setRequestProperty("Content-Type", "multipart/form-data;boundary=" + boundary);
                conn.setRequestProperty("uploaded_file", fileName); 

                dos = new DataOutputStream(conn.getOutputStream());

                dos.writeBytes(twoHyphens + boundary + lineEnd); 
                dos.writeBytes("Content-Disposition: form-data; name=\"uploaded_file\";filename="+ fileName + "" + lineEnd);
                dos.writeBytes(lineEnd);

                // create a buffer of  maximum size
                bytesAvailable = fileInputStream.available(); 

                bufferSize = Math.min(bytesAvailable, maxBufferSize);
                buffer = new byte[bufferSize];

                // read file and write it into form...
                bytesRead = fileInputStream.read(buffer, 0, bufferSize);  

                while (bytesRead > 0) {

                    dos.write(buffer, 0, bufferSize);
                    bytesAvailable = fileInputStream.available();
                    bufferSize = Math.min(bytesAvailable, maxBufferSize);
                    bytesRead = fileInputStream.read(buffer, 0, bufferSize);   

                }

                // send multipart form data necesssary after file data...
                dos.writeBytes(lineEnd);
                dos.writeBytes(twoHyphens + boundary + twoHyphens + lineEnd);

                // Responses from the server (code and message)
                serverResponseCode = conn.getResponseCode();
                //serverResponseCode = conn.getContent();
               // String serverResponseMessage = conn.getResponseMessage();
                String serverResponseMessage = conn.getContentEncoding();

                Log.i("uploadFile", "HTTP Response is 2 : "+ serverResponseMessage + ": " + serverResponseCode);
                
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                StringBuilder sb = new StringBuilder();
                String line;
                while ((line = br.readLine()) != null) {
                    sb.append(line+"\n");
                }
                br.close();
                JSONArray arry = new JSONArray(sb.toString());
                System.out.println(arry);
                JSONObject RESULT = arry.getJSONObject(0);
                FILEUPLOADRESULT2 = RESULT.get("Result").toString(); 

                if(serverResponseCode == 200){

                    runOnUiThread(new Runnable() {
                        public void run() {
                 
                            addCaseDocumentToRead2();
                        }
                    });
                    
                }    

                //close the streams //
                fileInputStream.close();
                dos.flush();
                dos.close();
                
                
                
                
                
                

            } catch (MalformedURLException ex) {

                //dialog.dismiss();  
                ex.printStackTrace();

                runOnUiThread(new Runnable() {
                    public void run() {

                        Toast.makeText(IndividualActivity.this, "MalformedURLException", 
                                Toast.LENGTH_LONG).show();
                    }
                });

                Log.e("Upload file to server", "error: " + ex.getMessage(), ex);  
            } catch (Exception e) {

                //dialog.dismiss();  
                e.printStackTrace();

                runOnUiThread(new Runnable() {
                    public void run() {

                        Toast.makeText(IndividualActivity.this, "Got Exception : see logcat ", 
                                Toast.LENGTH_LONG).show();
                    }
                });
                Log.e("Upload file to server Exception", "Exception : "
                        + e.getMessage(), e);  
            }
            //dialog.dismiss();       
            return serverResponseCode; 

        } // End else block 
    } 
	

	public int uploadFile(String sourceFileUri) {

        String fileName = sourceFileUri;

        HttpURLConnection conn = null;
        DataOutputStream dos = null;  
        String lineEnd = "\r\n";
        String twoHyphens = "--";
        String boundary = "*****";
        int bytesRead, bytesAvailable, bufferSize;
        byte[] buffer;
        int maxBufferSize = 1 * 1024 * 1024; 
        File sourceFile = new File(sourceFileUri); 

        if (!sourceFile.isFile()) {
            dialog.dismiss(); 
            Log.e("uploadFile", "Source File not exist :" +fileName);
            return 0;
        }
        else
        {
            try { 

                // open a URL connection to the Servlet
                FileInputStream fileInputStream = new FileInputStream(sourceFile);
                URL url = new URL(METHOD_ADDFILE);

                // Open a HTTP  connection to  the URL
                conn = (HttpURLConnection) url.openConnection(); 
                conn.setDoInput(true); // Allow Inputs
                conn.setDoOutput(true); // Allow Outputs
                conn.setUseCaches(false); // Don't use a Cached Copy
                conn.setRequestMethod("POST");
                conn.setRequestProperty("Connection", "Keep-Alive");
                conn.setRequestProperty("ENCTYPE", "multipart/form-data");
                conn.setRequestProperty("Content-Type", "multipart/form-data;boundary=" + boundary);
                conn.setRequestProperty("uploaded_file", fileName); 

                dos = new DataOutputStream(conn.getOutputStream());

                dos.writeBytes(twoHyphens + boundary + lineEnd); 
                dos.writeBytes("Content-Disposition: form-data; name=\"uploaded_file\";filename="+ fileName + "" + lineEnd);
                dos.writeBytes(lineEnd);

                // create a buffer of  maximum size
                bytesAvailable = fileInputStream.available(); 

                bufferSize = Math.min(bytesAvailable, maxBufferSize);
                buffer = new byte[bufferSize];

                // read file and write it into form...
                bytesRead = fileInputStream.read(buffer, 0, bufferSize);  

                while (bytesRead > 0) {

                    dos.write(buffer, 0, bufferSize);
                    bytesAvailable = fileInputStream.available();
                    bufferSize = Math.min(bytesAvailable, maxBufferSize);
                    bytesRead = fileInputStream.read(buffer, 0, bufferSize);   

                }

                // send multipart form data necesssary after file data...
                dos.writeBytes(lineEnd);
                dos.writeBytes(twoHyphens + boundary + twoHyphens + lineEnd);

                // Responses from the server (code and message)
                serverResponseCode = conn.getResponseCode();
                //serverResponseCode = conn.getContent();
               // String serverResponseMessage = conn.getResponseMessage();
                String serverResponseMessage = conn.getContentEncoding();

                Log.i("uploadFile", "HTTP Response is 1 : "+ serverResponseMessage + ": " + serverResponseCode);
                
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                StringBuilder sb = new StringBuilder();
                String line;
                while ((line = br.readLine()) != null) {
                    sb.append(line+"\n");
                }
                br.close();
                JSONArray arry = new JSONArray(sb.toString());
                System.out.println(arry);
                JSONObject RESULT = arry.getJSONObject(0);
                FILEUPLOADRESULT = RESULT.get("Result").toString(); 

                if(serverResponseCode == 200){

                    runOnUiThread(new Runnable() {
                        public void run() {
                 
                            addCaseDocumentToRead();
                        }
                    });
                    
                }    

                //close the streams //
                fileInputStream.close();
                dos.flush();
                dos.close();
                
                
                
                
                
                

            } catch (MalformedURLException ex) {

                //dialog.dismiss();  
                ex.printStackTrace();

                runOnUiThread(new Runnable() {
                    public void run() {

                        Toast.makeText(IndividualActivity.this, "MalformedURLException", 
                                Toast.LENGTH_LONG).show();
                    }
                });

                Log.e("Upload file to server", "error: " + ex.getMessage(), ex);  
            } catch (Exception e) {

                //dialog.dismiss();  
                e.printStackTrace();

                runOnUiThread(new Runnable() {
                    public void run() {

                        Toast.makeText(IndividualActivity.this, "Got Exception : see logcat ", 
                                Toast.LENGTH_LONG).show();
                    }
                });
                Log.e("Upload file to server Exception", "Exception : "
                        + e.getMessage(), e);  
            }
            //dialog.dismiss();       
            return serverResponseCode; 

        } // End else block 
    } 
	

	public void setallvalues2(Object data) throws JSONException {

		
		//JSONObject jObj =  new JSONObject(data.toString());
		JSONObject jObj = (JSONObject) data;
		//JSONObject jObj = arr.getJSONObject(0);
		
		System.out.println(jObj);
		individualIdNo3.setText(jObj.getString("IDNo3"));
		pdflink2=jObj.getString("ScanBackICLocation");
		/*EFullName.setText(jObj.getString("EmployeeName"));
		IDn1.setText(jObj.getString("IDNo1"));
		
		Taxno.setText(jObj.getString("TaxNo"));
		mobile.setText(jObj.getString("MobileNo"));
		Telephone.setText(jObj.getString("Telephone"));
		Office.setText(jObj.getString("OfficeNo"));
		idaddress1.setText(jObj.getString("IDAddress1"));
		idaddress2.setText(jObj.getString("IDAddress2"));
		idaddress3.setText(jObj.getString("IDAddress3"));
		idaddress4.setText(jObj.getString("IDAddress4"));
		idaddress5.setText(jObj.getString("IDAddress5"));
		comaddress1.setText(jObj.getString("CorresAddr1"));
		comaddress2.setText(jObj.getString("CorresAddr2"));
		comaddress3.setText(jObj.getString("CorresAddr3"));
		comaddress4.setText(jObj.getString("CorresAddr4"));
		comaddress5.setText(jObj.getString("CorresAddr5"));*/
		
		
	}
	
	
	public void slog(String s)
	{
		Toast.makeText(IndividualActivity.this, s, Toast.LENGTH_LONG).show();
	}
	
	
	public boolean dispatchTouchEvent(MotionEvent ev) {	       
        InputMethodManager imm = (InputMethodManager)getSystemService(Context.
                INPUT_METHOD_SERVICE);
imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
        return super.dispatchTouchEvent(ev);

        } 

}
