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
import android.annotation.SuppressLint;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.os.SystemClock;
import android.support.v7.app.AlertDialog;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ZoomButton;
import android.widget.AdapterView.OnItemSelectedListener;

@SuppressLint("NewApi")
public class PropertyActivity extends BaseActivity {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_ListofPropertyEnquiry
	// Key Pair Fields:-{
	// "TITLETYPE": "",
	// "TITLENO": "",
	// "LOTTYPE": "",
	// "LOT_NO": "",
	// "FORMERLY_KNOWN_AS": "",
	// "BPM": "",
	// "STATE": "",
	// "AREA": ""
	// }
	// Find list of property enquiry list web method
	private final String METHOD_LIST_PROPERTY = "SPA_ListofPropertyEnquiry";
	// Find list of propery enquiry edit details we method
	private final String METHOD_EDIT_PROPERTY = "SPA_EditPropertyEnquiryDetails";
	// Find list of propery enquiry edit details we method
	private final String METHOD_ADD_PROPERTY = "SPA_AddPropertyEnquiryDetails";
	// Find list of propery case list details web method
	private final String METHOD_PROPERTY_RELATED_CASELIST = "SPA_RelatedCases";
	// Find list of propery title type list dropdown web method
	private final String METHOD_PROPERTY_TITLETYPE_LIST_DROPDOWN = "SPA_GetValidValues";
	// Find list of propery list dropdown web method
	private final String METHOD_PROPERTY_LIST_DROPDOWN = "SPA_GetProject";
	// Find list of propery bank developer solicitor list dropdown web method
	private final String METHOD_PROPERTY_BDS_DROPDOWN = "SPA_Property_GetDropdownValues";

	// Find property enquiry list Strings
	private String code = "", category = "", titleTYPE = "", titleNO = "", lotTYPE = "", lot_NO = "",
			formerly_KNOWN_AS = "", bpm = "", state = "", area = "";
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	// Find Edit fields
	EditText propertytitleNo, propertyLotType, propertyLotPTDNo, propertyFormerlyKnownAs, propertyBandarPekanMukin,
			propertyDaerahState, propertyNageriArea;

	EditText propertyLOTAREA_SQM, propertyLOTAREA_SQFT, propertyLASTUPDATEDON, propertyDVLPR_CODE, propertyDEVLICNO;

	EditText propertyDVLPR_SOL_CODE, propertyDVLPR_LOC, propertyLSTCHG_BANKCODE, propertyLSTCHG_BRANCH,
			propertyLSTCHG_PANO, propertyLSTCHG_PRSTNO;
	// Find spinner fields
	Spinner spinnerpropertyTitleType, spinnerpropertyPROJECT, spinnerpropertyLSTCHG_BANKNAME, spinnerpropertyDEVELOPER,
			spinnerpropertyDEVSOLICTOR;

	// Find String code value from
	String codeResonse_listview;
	Button buttonWalkIn, buttonFind, buttonAdd, buttonEdit, buttonConfirm, buttonPropertyList, buttonRelatedCaes;
	ZoomButton ZoomButton_propertyPdf1;
	ListView listView_property;
	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;
	ArrayList<HashMap<String, String>> jsonlist;
	private long mLastClickTime = 0;

	Boolean isADD = false, isEdit = false, isPropertyList = false, isPropertycaseList = false;
	String messageDisplay = "", StatusResult = "";
	String codeDetailResponse = "", titleTypeDetailResponse = "", titleNoDetailResponse = "",
			lotTypeDetailResponse = "", lotNoDetailResponse = "", formerlyDetailResponse = "", bpmDetailResponse = "",
			stateDetailResponse = "", areaDetailResponse = "", lotAreaDetailResponse = "",
			lotaresSoftDetailResponse = "", lastupDateDetailResponse = "", developerCodeResponse = "",
			developerDetailResponse = "", projectDetailResponse = "", DevlicNoDetailResponse = "",
			devSolictorCodeResponse = "", devSolictorDetailResponse = "", devSolictorLocDetailResponse = "",
			bankCodeResponse = "", bankDetailResponse = "", branchDetailResponse = "", panNoDetailResponse = "",
			prsentDetailResponse = "";

	// Find Case list items
	String CaseList_CaseFileNo = "", CaseList_RelatedFileNo = "", CaseList_BranchCode = "",
			CaseList_FileOpenedDate = "", CaseList_IC = "", CaseList_CaseType = "", CaseList_ClientName = "",
			CaseList_BankName = "", CaseList_Branch = "", CaseList_LOTNo = "", CaseList_CaseAmount = "",
			CaseList_UserCode = "", CaseList_Status = "", CaseList_FileClosedDate = "";

	ArrayList<HashMap<String, String>> jsonCaselist;
	// Find dropdpwn list
	ArrayList<HashMap<String, String>> jsonlistProject = null, jsonlistProjectTitle = null, jsonlistBank = null,
			jsonlistDeveloper = null, jsonlistSolicitor = null;
	String id, name, id_b, name_b, id_d, name_d, id_s, name_s;
	SimpleAdapter sAdap = null, sAdapTYPE = null, sAdapPROJ = null, sAdapBANK = null, sAdapDEV = null,
			sAdapSOLIC = null;
	TextView textTitle_id, textProject_id, textBank_id, textDeveloper_id, textSolicitor_id, textTitle, textProject,
			textBank, textDeveloper, textSolicitor;
	String titleValue_id = "", projectValue_id = "", bankValue_id = "", developerValue_id = "", solicitorValue_id = "",
			titleValue = "", projectValue = "", bankValue = "", developerValue = "", solicitorValue = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_property);

		// load titles from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		// Find Set Function
		set(navMenuTitles, navMenuIcons);
		// Find Button by Id
		buttonWalkIn = (Button) findViewById(R.id.button_PropertyWalkin);
		buttonFind = (Button) findViewById(R.id.button_PropertyFind);
		buttonAdd = (Button) findViewById(R.id.button_PropertyAdd);
		buttonEdit = (Button) findViewById(R.id.button_PropertyEdit);
		buttonConfirm = (Button) findViewById(R.id.button_PropertyConfirm);
		buttonPropertyList = (Button) findViewById(R.id.button_PropertyList);
		buttonRelatedCaes = (Button) findViewById(R.id.button_PropertyRelateCases);
		ZoomButton_propertyPdf1 = (ZoomButton) findViewById(R.id.zoomButton_propertyPdf1);

		// Find EditText Fields
		propertytitleNo = (EditText) findViewById(R.id.editText_ProperTytitleNo);
		propertyLotType = (EditText) findViewById(R.id.editText_PropertyLotType);
		propertyLotPTDNo = (EditText) findViewById(R.id.editText_PropertyLotPTDNo);
		propertyFormerlyKnownAs = (EditText) findViewById(R.id.editText_PropertyFormerlyKnownAs);
		propertyBandarPekanMukin = (EditText) findViewById(R.id.editText_PropertyBandarPekanMukin);
		propertyDaerahState = (EditText) findViewById(R.id.editText_PropertyDaerahState);
		propertyNageriArea = (EditText) findViewById(R.id.editText_PropertyNageriArea);

		propertyLOTAREA_SQM = (EditText) findViewById(R.id.editText_PropertyLotArea);
		propertyLOTAREA_SQFT = (EditText) findViewById(R.id.editText_PropertySqMeter);
		propertyLASTUPDATEDON = (EditText) findViewById(R.id.editText_PropertyLastUpdateOn);

		propertyDEVLICNO = (EditText) findViewById(R.id.editText_PropertyDevLicense);
		propertyDVLPR_LOC = (EditText) findViewById(R.id.editText_PropertySolicitorLoc);
		propertyLSTCHG_BRANCH = (EditText) findViewById(R.id.editText_PropertyBranch);
		propertyLSTCHG_PANO = (EditText) findViewById(R.id.editText_PropertyPAName);
		propertyLSTCHG_PRSTNO = (EditText) findViewById(R.id.editText_PropertyPresentaionNo);

		// Find By Id spinner Address To Use
		spinnerpropertyTitleType = (Spinner) findViewById(R.id.spinner_PropertyTitleType);
		spinnerpropertyPROJECT = (Spinner) findViewById(R.id.spinner_PropertyProjectDropdown);
		spinnerpropertyLSTCHG_BANKNAME = (Spinner) findViewById(R.id.spinner_PropertyProjectBank);
		spinnerpropertyDEVELOPER = (Spinner) findViewById(R.id.spinner_PropertyDevelopoer);
		spinnerpropertyDEVSOLICTOR = (Spinner) findViewById(R.id.spinner_PropertySolicitor);

		// Spinner click listener
		spinnerpropertyTitleType.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
				textTitle_id = (TextView) view.findViewById(R.id.Id);
				titleValue_id = textTitle_id.getText().toString();
				textTitle = (TextView) view.findViewById(R.id.Name);
				titleValue = textTitle.getText().toString();

				// Showing selected spinner item
				Toast.makeText(parent.getContext(), "Selected: " + titleValue, Toast.LENGTH_LONG).show();

			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub

			}
		});

		// Spinner click listener
		spinnerpropertyPROJECT.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
				textProject_id = (TextView) view.findViewById(R.id.Id);
				projectValue_id = textProject_id.getText().toString();
				textProject = (TextView) view.findViewById(R.id.Name);
				projectValue = textProject.getText().toString();

				// Showing selected spinner item
				Toast.makeText(parent.getContext(), "Selected: " + projectValue, Toast.LENGTH_LONG).show();

			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub

			}
		});

		// Spinner click listener
		spinnerpropertyDEVELOPER.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
				textDeveloper_id = (TextView) view.findViewById(R.id.Id);
				developerValue_id = textDeveloper_id.getText().toString();
				textDeveloper = (TextView) view.findViewById(R.id.Name);
				developerValue = textDeveloper.getText().toString();

				// Showing selected spinner item
				Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub

			}
		});
		// Spinner click listener
		spinnerpropertyDEVSOLICTOR.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
				textSolicitor_id = (TextView) view.findViewById(R.id.Id);
				solicitorValue_id = textSolicitor_id.getText().toString();
				textSolicitor = (TextView) view.findViewById(R.id.Name);
				solicitorValue = textSolicitor.getText().toString();

				// Showing selected spinner item
				Toast.makeText(parent.getContext(), "Selected: " + solicitorValue, Toast.LENGTH_LONG).show();

			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub

			}
		});

		// Spinner click listener
		spinnerpropertyLSTCHG_BANKNAME.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
				textBank_id = (TextView) view.findViewById(R.id.Id);
				bankValue_id = textBank_id.getText().toString();
				textBank = (TextView) view.findViewById(R.id.Name);

				bankValue = textBank.getText().toString();

				// Showing selected spinner item
				Toast.makeText(parent.getContext(), "Selected: " + bankValue, Toast.LENGTH_LONG).show();

			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// TODO Auto-generated method stub

			}
		});

		spinnerpropertyTitleType.requestFocus();
		// Find disable spinner
		spinnerpropertyDEVELOPER.setEnabled(false);
		spinnerpropertyDEVELOPER.setClickable(false);

		spinnerpropertyPROJECT.setEnabled(false);
		spinnerpropertyPROJECT.setClickable(false);

		spinnerpropertyDEVSOLICTOR.setEnabled(false);
		spinnerpropertyDEVSOLICTOR.setClickable(false);

		spinnerpropertyLSTCHG_BANKNAME.setEnabled(false);
		spinnerpropertyLSTCHG_BANKNAME.setClickable(false);

		// PropertyWalkin button enable
		buttonWalkIn.setEnabled(true);
		buttonWalkIn.setClickable(true);
		buttonWalkIn.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

		// Find button enable
		buttonFind.setClickable(false);
		buttonFind.setTextColor(getApplication().getResources().getColor(R.color.gray));

		// PropertyList button enable
		buttonPropertyList.setEnabled(true);
		buttonPropertyList.setClickable(true);
		buttonPropertyList.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

		buttonFind.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				spinnerpropertyTitleType.requestFocus();
				spinnerpropertyTitleType.setFocusableInTouchMode(true);

				// find button disable
				buttonFind.setClickable(false);
				buttonFind.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// Property Related case list button enable
				buttonRelatedCaes.setEnabled(false);
				buttonRelatedCaes.setClickable(false);
				buttonRelatedCaes.setTextColor(getApplication().getResources().getColor(R.color.gray));

			}
		});

		// Add button enable
		buttonAdd.setClickable(true);
		buttonAdd.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

		buttonAdd.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				// change boolean flag
				isADD = true;

				// Add button disable
				buttonAdd.setClickable(false);
				buttonAdd.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// Walk-in button enable
				buttonWalkIn.setEnabled(true);
				buttonWalkIn.setClickable(true);
				buttonWalkIn.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

				// Confirm button enable
				buttonConfirm.setEnabled(true);
				buttonConfirm.setClickable(true);
				buttonConfirm.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

				// Property List button enable
				buttonPropertyList.setEnabled(false);
				buttonPropertyList.setClickable(false);
				buttonPropertyList.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// Related button enable
				buttonRelatedCaes.setEnabled(false);
				buttonRelatedCaes.setClickable(false);
				buttonRelatedCaes.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// Related button enable
				buttonEdit.setEnabled(false);
				buttonEdit.setClickable(false);
				buttonEdit.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// Set edit text enable
				spinnerpropertyTitleType.setEnabled(true);
				spinnerpropertyTitleType.setClickable(true);
				spinnerpropertyTitleType.setFocusable(true);
				spinnerpropertyTitleType.setFocusableInTouchMode(true);

				propertytitleNo.setEnabled(true);
				propertytitleNo.setClickable(true);
				propertytitleNo.setFocusableInTouchMode(true);
				propertytitleNo.setText("");

				propertyLotType.setEnabled(true);
				propertyLotType.setClickable(true);
				propertyLotType.setFocusableInTouchMode(true);
				propertyLotType.setText("");

				propertyLotPTDNo.setEnabled(true);
				propertyLotPTDNo.setClickable(true);
				propertyLotPTDNo.setFocusableInTouchMode(true);
				propertyLotPTDNo.setText("");

				propertyFormerlyKnownAs.setEnabled(true);
				propertyFormerlyKnownAs.setClickable(true);
				propertyFormerlyKnownAs.setFocusableInTouchMode(true);
				propertyFormerlyKnownAs.setText("");

				propertyBandarPekanMukin.setEnabled(true);
				propertyBandarPekanMukin.setClickable(true);
				propertyBandarPekanMukin.setFocusableInTouchMode(true);
				propertyBandarPekanMukin.setText("");

				propertyDaerahState.setEnabled(true);
				propertyDaerahState.setClickable(true);
				propertyDaerahState.setFocusableInTouchMode(true);
				propertyDaerahState.setText("");

				propertyNageriArea.setEnabled(true);
				propertyNageriArea.setClickable(true);
				propertyNageriArea.setFocusableInTouchMode(true);
				propertyNageriArea.setText("");

				propertyLOTAREA_SQM.setEnabled(true);
				propertyLOTAREA_SQM.setClickable(true);
				propertyLOTAREA_SQM.setFocusableInTouchMode(true);
				propertyLOTAREA_SQM.setText("");

				propertyLOTAREA_SQFT.setEnabled(true);
				propertyLOTAREA_SQFT.setClickable(true);
				propertyLOTAREA_SQFT.setFocusableInTouchMode(true);
				propertyLOTAREA_SQFT.setText("");

				propertyLASTUPDATEDON.setEnabled(false);
				propertyLASTUPDATEDON.setClickable(false);
				propertyLASTUPDATEDON.setFocusableInTouchMode(false);
				propertyLASTUPDATEDON.setText("");

				spinnerpropertyDEVELOPER.setEnabled(true);
				spinnerpropertyDEVELOPER.setClickable(true);
				spinnerpropertyDEVELOPER.setFocusableInTouchMode(true);

				spinnerpropertyPROJECT.setEnabled(true);
				spinnerpropertyPROJECT.setClickable(true);
				spinnerpropertyPROJECT.setFocusableInTouchMode(true);

				propertyDEVLICNO.setEnabled(true);
				propertyDEVLICNO.setClickable(true);
				propertyDEVLICNO.setFocusableInTouchMode(true);
				propertyDEVLICNO.setText("");

				spinnerpropertyDEVSOLICTOR.setEnabled(true);
				spinnerpropertyDEVSOLICTOR.setClickable(true);
				spinnerpropertyDEVSOLICTOR.setFocusableInTouchMode(true);

				propertyDVLPR_LOC.setEnabled(true);
				propertyDVLPR_LOC.setClickable(true);
				propertyDVLPR_LOC.setFocusableInTouchMode(true);
				propertyDVLPR_LOC.setText("");

				spinnerpropertyLSTCHG_BANKNAME.setEnabled(true);
				spinnerpropertyLSTCHG_BANKNAME.setClickable(true);
				spinnerpropertyLSTCHG_BANKNAME.setFocusableInTouchMode(true);

				propertyLSTCHG_BRANCH.setEnabled(true);
				propertyLSTCHG_BRANCH.setClickable(true);
				propertyLSTCHG_BRANCH.setFocusableInTouchMode(true);
				propertyLSTCHG_BRANCH.setText("");

				propertyLSTCHG_PANO.setEnabled(true);
				propertyLSTCHG_PANO.setClickable(true);
				propertyLSTCHG_PANO.setFocusableInTouchMode(true);
				propertyLSTCHG_PANO.setText("");

				propertyLSTCHG_PRSTNO.setEnabled(true);
				propertyLSTCHG_PRSTNO.setClickable(true);
				propertyLSTCHG_PRSTNO.setFocusableInTouchMode(true);
				propertyLSTCHG_PRSTNO.setText("");

			}
		});

		/*
		 * buttonWalkIn.setOnClickListener(new OnClickListener() {
		 * 
		 * @Override public void onClick(View v) { // TODO Auto-generated method
		 * stub Intent intentWalkIn = new Intent(PropertyActivity.this,
		 * WalkInActivity.class); startActivity(intentWalkIn);
		 * 
		 * } });
		 */

		// Find ZoomButton for property list
		ZoomButton_propertyPdf1.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

			}
		});

		// Find Button for edit property details

		buttonEdit.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				// change the boolean flag
				isEdit = true;

				// Confirm button enable
				buttonConfirm.setEnabled(true);
				buttonConfirm.setClickable(true);
				buttonConfirm.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

				// Add button disable
				buttonAdd.setEnabled(false);
				buttonAdd.setClickable(false);
				buttonAdd.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// Edit button disable
				buttonEdit.setEnabled(false);
				buttonEdit.setClickable(false);
				buttonEdit.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// Walk-in button disable
				buttonWalkIn.setEnabled(false);
				buttonWalkIn.setClickable(false);
				buttonWalkIn.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// PropertyList button disable
				buttonPropertyList.setEnabled(false);
				buttonPropertyList.setClickable(false);
				buttonPropertyList.setTextColor(getApplication().getResources().getColor(R.color.gray));

				// Related case button enable
				buttonRelatedCaes.setEnabled(true);
				buttonRelatedCaes.setClickable(true);
				buttonRelatedCaes.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

				// edit text enable
				spinnerpropertyTitleType.setClickable(true);
				spinnerpropertyTitleType.setEnabled(true);
				spinnerpropertyTitleType.setFocusableInTouchMode(true);

				propertytitleNo.setClickable(true);
				propertytitleNo.setEnabled(true);
				propertytitleNo.setFocusableInTouchMode(true);

				propertyLotType.setClickable(true);
				propertyLotType.setEnabled(true);
				propertyLotType.setFocusableInTouchMode(true);

				propertyLotPTDNo.setClickable(true);
				propertyLotPTDNo.setEnabled(true);
				propertyLotPTDNo.setFocusableInTouchMode(true);

				propertyFormerlyKnownAs.setClickable(true);
				propertyFormerlyKnownAs.setEnabled(true);
				propertyFormerlyKnownAs.setFocusableInTouchMode(true);

				propertyBandarPekanMukin.setClickable(true);
				propertyBandarPekanMukin.setEnabled(true);
				propertyBandarPekanMukin.setFocusableInTouchMode(true);

				propertyDaerahState.setClickable(true);
				propertyDaerahState.setEnabled(true);
				propertyDaerahState.setFocusableInTouchMode(true);

				propertyNageriArea.setClickable(true);
				propertyNageriArea.setEnabled(true);
				propertyNageriArea.setFocusableInTouchMode(true);

				propertyLOTAREA_SQM.setClickable(true);
				propertyLOTAREA_SQM.setEnabled(true);
				propertyLOTAREA_SQM.setFocusableInTouchMode(true);

				propertyLOTAREA_SQFT.setClickable(true);
				propertyLOTAREA_SQFT.setEnabled(true);
				propertyLOTAREA_SQFT.setFocusableInTouchMode(true);

				propertyLASTUPDATEDON.setClickable(false);
				propertyLASTUPDATEDON.setEnabled(false);
				propertyLASTUPDATEDON.setFocusableInTouchMode(false);

				spinnerpropertyDEVELOPER.setClickable(true);
				spinnerpropertyDEVELOPER.setEnabled(true);
				spinnerpropertyDEVELOPER.setFocusable(true);
				spinnerpropertyDEVELOPER.setFocusableInTouchMode(true);

				spinnerpropertyPROJECT.setClickable(true);
				spinnerpropertyPROJECT.setEnabled(true);
				spinnerpropertyPROJECT.setFocusableInTouchMode(true);

				propertyDEVLICNO.setClickable(true);
				propertyDEVLICNO.setEnabled(true);
				propertyDEVLICNO.setFocusableInTouchMode(true);

				spinnerpropertyDEVSOLICTOR.setClickable(true);
				spinnerpropertyDEVSOLICTOR.setEnabled(true);
				spinnerpropertyDEVSOLICTOR.setFocusableInTouchMode(true);

				propertyDVLPR_LOC.setClickable(true);
				propertyDVLPR_LOC.setEnabled(true);
				propertyDVLPR_LOC.setFocusableInTouchMode(true);

				spinnerpropertyLSTCHG_BANKNAME.setClickable(true);
				spinnerpropertyLSTCHG_BANKNAME.setEnabled(true);
				spinnerpropertyLSTCHG_BANKNAME.setFocusableInTouchMode(true);

				propertyLSTCHG_BRANCH.setClickable(true);
				propertyLSTCHG_BRANCH.setEnabled(true);
				propertyLSTCHG_BRANCH.setFocusableInTouchMode(true);

				propertyLSTCHG_PANO.setClickable(true);
				propertyLSTCHG_PANO.setEnabled(true);
				propertyLSTCHG_PANO.setFocusableInTouchMode(true);

				propertyLSTCHG_PRSTNO.setClickable(true);
				propertyLSTCHG_PRSTNO.setEnabled(true);
				propertyLSTCHG_PRSTNO.setFocusableInTouchMode(true);

			}
		});

		// Find Confirm button

		buttonConfirm.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(PropertyActivity.this);

				// set title
				alertDialogBuilder.setTitle("Confirm");

				// set dialog message
				alertDialogBuilder.setMessage("Click yes to save!").setCancelable(false)
						.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int id) {

						if (isADD == true && isEdit == false) {
							// call edit webservice for property details
							addDatapropertyDetails();

						} else {

							// call edit webservice for property details
							sendDataEditpropertyDetails();

						}
						Intent i = getIntent();
						startActivity(i);

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

		// Find Property List on button Click

		buttonPropertyList.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				spinnerpropertyTitleType.setEnabled(true);
				spinnerpropertyTitleType.requestFocus();
				spinnerpropertyTitleType.setClickable(true);
				spinnerpropertyTitleType.setFocusable(true);

				Toast.makeText(PropertyActivity.this, "Search button clicked!", Toast.LENGTH_SHORT).show();
				// Call web service
				webRequestPropertyDetails();
			}
		});

		// Find Property Case List on button click

		buttonRelatedCaes.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				webRequestPropertyCaseList();
				Toast.makeText(PropertyActivity.this, "Related case clicked", Toast.LENGTH_SHORT).show();
			}
		});

		// Result Bundle getting from list item click to property activity
		Bundle b = getIntent().getExtras();
		if (b != null) {

			// Add button disable
			buttonAdd.setClickable(true);
			buttonAdd.setFocusable(true);
			buttonAdd.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

			// Find button enable
			buttonFind.setClickable(false);
			buttonFind.setFocusable(false);
			buttonFind.setTextColor(getApplication().getResources().getColor(R.color.gray));

			// PropertyList button enable
			buttonPropertyList.setClickable(false);
			buttonPropertyList.setFocusable(false);
			buttonPropertyList.setTextColor(getApplication().getResources().getColor(R.color.gray));
			// Related Case button enable
			buttonRelatedCaes.setClickable(true);
			buttonRelatedCaes.setFocusable(true);
			buttonRelatedCaes.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

			// Edit button enable
			buttonEdit.setClickable(true);
			buttonEdit.setFocusable(true);
			buttonEdit.setTextColor(getApplication().getResources().getColor(R.color.royalBlue));

			codeDetailResponse = b.getString("CODE_T");
			System.out.println("Property Text Details");
			System.out.println(codeDetailResponse);
			codeResonse_listview = codeDetailResponse;

			titleTypeDetailResponse = b.getString("TITLETYPE_T");
			System.out.println(titleTypeDetailResponse);
			spinnerpropertyTitleType.setEnabled(false);
			spinnerpropertyTitleType.setClickable(false);

			titleNoDetailResponse = b.getString("TITLENO_T");
			System.out.println(titleNoDetailResponse);
			propertytitleNo.setEnabled(false);
			propertytitleNo.setClickable(false);
			propertytitleNo.setText(titleNoDetailResponse);

			lotTypeDetailResponse = b.getString("LOTTYPE_T");
			System.out.println(lotTypeDetailResponse);
			propertyLotType.setEnabled(false);
			propertyLotType.setClickable(false);
			propertyLotType.setText(lotTypeDetailResponse);

			lotNoDetailResponse = b.getString("LOTNO_T");
			System.out.println(lotNoDetailResponse);
			propertyLotPTDNo.setEnabled(false);
			propertyLotPTDNo.setClickable(false);
			propertyLotPTDNo.setText(lotNoDetailResponse);

			formerlyDetailResponse = b.getString("FORMERLY_KNOWN_AS_T");
			System.out.println(formerlyDetailResponse);
			propertyFormerlyKnownAs.setEnabled(false);
			propertyFormerlyKnownAs.setClickable(false);
			propertyFormerlyKnownAs.setText(formerlyDetailResponse);

			bpmDetailResponse = b.getString("BPM_T");
			System.out.println(bpmDetailResponse);
			propertyBandarPekanMukin.setEnabled(false);
			propertyBandarPekanMukin.setClickable(false);
			propertyBandarPekanMukin.setText(bpmDetailResponse);

			stateDetailResponse = b.getString("STATE_T");
			System.out.println(stateDetailResponse);
			propertyDaerahState.setEnabled(false);
			propertyDaerahState.setClickable(false);
			propertyDaerahState.setText(stateDetailResponse);

			areaDetailResponse = b.getString("AREA_T");
			System.out.println(areaDetailResponse);
			propertyNageriArea.setEnabled(false);
			propertyNageriArea.setClickable(false);
			propertyNageriArea.setText(areaDetailResponse);

			lotAreaDetailResponse = b.getString("LOTAREA_SQM_T");
			System.out.println(lotAreaDetailResponse);
			propertyLOTAREA_SQM.setEnabled(false);
			propertyLOTAREA_SQM.setClickable(false);
			propertyLOTAREA_SQM.setText(lotAreaDetailResponse);

			lotaresSoftDetailResponse = b.getString("LOTAREA_SQFT_T");
			System.out.println(lotaresSoftDetailResponse);
			propertyLOTAREA_SQFT.setEnabled(false);
			propertyLOTAREA_SQFT.setClickable(false);
			propertyLOTAREA_SQFT.setText(lotaresSoftDetailResponse);

			lastupDateDetailResponse = b.getString("LASTUPDATEDON_T");
			System.out.println(lastupDateDetailResponse);
			propertyLASTUPDATEDON.setEnabled(false);
			propertyLASTUPDATEDON.setClickable(false);
			propertyLASTUPDATEDON.setText(lastupDateDetailResponse);

			developerDetailResponse = b.getString("DEVELOPER_T");
			System.out.println(developerDetailResponse);
			spinnerpropertyDEVELOPER.setEnabled(false);
			spinnerpropertyDEVELOPER.setClickable(false);

			developerCodeResponse = b.getString("DVLPR_CODE_T");
			System.out.println(developerCodeResponse);
			spinnerpropertyDEVELOPER.setEnabled(false);
			spinnerpropertyDEVELOPER.setClickable(false);

			projectDetailResponse = b.getString("PROJECT_T");
			System.out.println(projectDetailResponse);
			spinnerpropertyPROJECT.setEnabled(false);
			spinnerpropertyPROJECT.setClickable(false);

			DevlicNoDetailResponse = b.getString("DEVLICNO_T");
			System.out.println(DevlicNoDetailResponse);
			propertyDEVLICNO.setEnabled(false);
			propertyDEVLICNO.setClickable(false);
			propertyDEVLICNO.setText(DevlicNoDetailResponse);

			devSolictorDetailResponse = b.getString("DEVSOLICTOR_T");
			System.out.println(devSolictorDetailResponse);
			spinnerpropertyDEVSOLICTOR.setEnabled(false);
			spinnerpropertyDEVSOLICTOR.setClickable(false);

			devSolictorCodeResponse = b.getString("DVLPR_SOL_CODE_T");
			System.out.println(devSolictorCodeResponse);
			spinnerpropertyDEVSOLICTOR.setEnabled(false);
			spinnerpropertyDEVSOLICTOR.setClickable(false);

			devSolictorLocDetailResponse = b.getString("DVLPR_LOC_T");
			System.out.println(devSolictorLocDetailResponse);
			propertyDVLPR_LOC.setEnabled(false);
			propertyDVLPR_LOC.setClickable(false);
			propertyDVLPR_LOC.setText(devSolictorLocDetailResponse);

			bankCodeResponse = b.getString("LSTCHG_BANKNAME_T");
			System.out.println(bankCodeResponse);
			spinnerpropertyLSTCHG_BANKNAME.setEnabled(false);
			spinnerpropertyLSTCHG_BANKNAME.setClickable(false);

			bankDetailResponse = b.getString("LSTCHG_BANKNAME_T");
			System.out.println(bankDetailResponse);
			spinnerpropertyLSTCHG_BANKNAME.setEnabled(false);
			spinnerpropertyLSTCHG_BANKNAME.setClickable(false);

			branchDetailResponse = b.getString("LSTCHG_BRANCH_T");
			System.out.println(branchDetailResponse);
			propertyLSTCHG_BRANCH.setEnabled(false);
			propertyLSTCHG_BRANCH.setClickable(false);
			propertyLSTCHG_BRANCH.setText(branchDetailResponse);

			panNoDetailResponse = b.getString("LSTCHG_PANO_T");
			System.out.println(panNoDetailResponse);
			propertyLSTCHG_PANO.setEnabled(false);
			propertyLSTCHG_PANO.setClickable(false);
			propertyLSTCHG_PANO.setText(panNoDetailResponse);

			prsentDetailResponse = b.getString("LSTCHG_PRSTNO_T");
			System.out.println(prsentDetailResponse);
			propertyLSTCHG_PRSTNO.setEnabled(false);
			propertyLSTCHG_PRSTNO.setClickable(false);
			propertyLSTCHG_PRSTNO.setText(prsentDetailResponse);

		}
		// Dropdown function title type
		dropdownPorjectTitleType();
		// Dropdown function project
		dropdownPorject();
		// Dropdown BankDeveloperSolicitor function
		dropdownBankDeveloperSolicitor();
	}

	public void dropdownPorjectTitleType() {
		// Passing value in JSON format in first 8-fields

		/* { "TableName": "@AE_PROPERTY", "FieldName": "TITLETYPE" } */
		JSONObject jsonObject = new JSONObject();

		try {
			jsonObject.put("TableName", "@AE_PROPERTY");
			jsonObject.put("FieldName", "TITLETYPE");

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_PROPERTY_TITLETYPE_LIST_DROPDOWN, params,
					new BaseJsonHttpResponseHandler<String>() {

						@Override
						public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
							// TODO Auto-generated method stub
							System.out.println(arg3);

						}

						@Override
						public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
							// TODO Auto-generated method stub
							System.out.println("propertyTitle Type Dropdown Success Details ");
							System.out.println(arg2);

							try {

								arrayResponse = new JSONArray(arg2);
								// Create new list
								jsonlistProjectTitle = new ArrayList<HashMap<String, String>>();

								for (int i = 0; i < arrayResponse.length(); i++) {

									jsonResponse = arrayResponse.getJSONObject(i);

									id = jsonResponse.getString("Id").toString();
									name = jsonResponse.getString("Name").toString();

									// SEND JSON DATA INTO SPINNER TITLE LIST
									HashMap<String, String> proList = new HashMap<String, String>();

									// Send JSON Data to list activity
									System.out.println("SEND JSON TITLE TYPE LIST");
									proList.put("Id_T", id);
									System.out.println(name);
									proList.put("Name_T", name);
									System.out.println(name);
									System.out.println(" END SEND JSON PROPERTY TITLE TYPE LIST");

									jsonlistProjectTitle.add(proList);
									System.out.println("JSON PROPERTY LIST");
									System.out.println(jsonlistProjectTitle);
								}
								// Spinner set Array Data in Drop down

								sAdapTYPE = new SimpleAdapter(PropertyActivity.this, jsonlistProjectTitle,
										R.layout.spinner_item, new String[] { "Id_T", "Name_T" },
										new int[] { R.id.Id, R.id.Name });

								spinnerpropertyTitleType.setAdapter(sAdapTYPE);

								for (int j = 0; j < jsonlistProjectTitle.size(); j++) {
									if (jsonlistProjectTitle.get(j).get("Name_T").equals(titleTypeDetailResponse)) {
										spinnerpropertyTitleType.setSelection(j);
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

							System.out.println("Property Title Type Dropdown Details parse Response");
							System.out.println(arg0);
							return null;
						}
					});
		} catch (JSONException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
	}

	public void dropdownBankDeveloperSolicitor() {

		RequestParams params = null;
		params = new RequestParams();

		RestService.post(METHOD_PROPERTY_BDS_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Property Activity GetDropdown Success Details ");
				try {
					// Create new list
					jsonlistBank = new ArrayList<HashMap<String, String>>();
					jsonlistDeveloper = new ArrayList<HashMap<String, String>>();
					jsonlistSolicitor = new ArrayList<HashMap<String, String>>();

					jsonResponse = new JSONObject(arg2);

					JSONArray jsonBank = jsonResponse.getJSONArray("Bank");
					for (int j = 0; j < jsonBank.length(); j++) {
						JSONObject bank = jsonBank.getJSONObject(j);

						id_b = bank.getString("BankCode").toString();
						name_b = bank.getString("BankName").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> bankList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON BANK LIST");
						bankList.put("Id_T", id_b);
						System.out.println(name);
						bankList.put("Name_T", name_b);
						System.out.println(name);
						System.out.println(" END SEND JSON BANK LIST");

						jsonlistBank.add(bankList);
						System.out.println("JSON BANK LIST");
						System.out.println(jsonlistProject);

					}

					// Spinner set Array Data in Drop down

					sAdapBANK = new SimpleAdapter(PropertyActivity.this, jsonlistBank, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyLSTCHG_BANKNAME.setAdapter(sAdapBANK);

					for (int j = 0; j < jsonlistBank.size(); j++) {
						if (jsonlistBank.get(j).get("Name_T").equals(bankDetailResponse)) {
							spinnerpropertyLSTCHG_BANKNAME.setSelection(j);
							break;
						}
					}

					JSONArray jsonDeveloper = jsonResponse.getJSONArray("Developer");
					for (int j = 0; j < jsonDeveloper.length(); j++) {
						JSONObject dev = jsonDeveloper.getJSONObject(j);
						id = dev.getString("DevCode").toString();
						name = dev.getString("DevName").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> devList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON DEV LIST");
						devList.put("Id_B", id);
						System.out.println(name);
						devList.put("Name_B", name);
						System.out.println(name);
						System.out.println(" END SEND JSON DEV LIST");

						jsonlistDeveloper.add(devList);
						System.out.println("JSON DEV LIST");
						System.out.println(jsonlistDeveloper);

					}
					// Spinner set Array Data in Drop down

					sAdapDEV = new SimpleAdapter(PropertyActivity.this, jsonlistDeveloper, R.layout.spinner_item,
							new String[] { "Id_B", "Name_B" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyDEVELOPER.setAdapter(sAdapDEV);

					for (int j = 0; j < jsonlistDeveloper.size(); j++) {
						if (jsonlistDeveloper.get(j).get("Id_B").equals(developerCodeResponse)) {
							spinnerpropertyDEVELOPER.setSelection(j);
							break;
						}
					}

					JSONArray jsonSolicitor = jsonResponse.getJSONArray("Solicitor");
					for (int j = 0; j < jsonSolicitor.length(); j++) {
						JSONObject solic = jsonSolicitor.getJSONObject(j);
						id = solic.getString("SoliCode").toString();
						name = solic.getString("SoliName").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> solicList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON SOLICITOR LIST");
						solicList.put("Id_T", id);
						System.out.println(name);
						solicList.put("Name_T", name);
						System.out.println(name);
						System.out.println(" END SEND JSON SOLICITOR LIST");

						jsonlistSolicitor.add(solicList);
						System.out.println("JSON SOLICITOR LIST");
						System.out.println(jsonlistSolicitor);
					}

					// Spinner set Array Data in Drop down
					sAdapSOLIC = new SimpleAdapter(PropertyActivity.this, jsonlistSolicitor, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyDEVSOLICTOR.setAdapter(sAdapSOLIC);

					for (int j = 0; j < jsonlistSolicitor.size(); j++) {
						if (jsonlistSolicitor.get(j).get("Id_T").equals(devSolictorCodeResponse)) {
							spinnerpropertyDEVSOLICTOR.setSelection(j);
							break;
						}
					}

				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}

				System.out.println(arg2);

			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {
				// Get Json response

				System.out.println("Property GetDropdown parse Response");
				System.out.println(arg0);
				return null;
			}

		});

	}

	public void dropdownPorject() {

		RequestParams params = null;
		params = new RequestParams();

		RestService.post(METHOD_PROPERTY_LIST_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("property Dropdown Success Details ");
				System.out.println(arg2);

				try {

					arrayResponse = new JSONArray(arg2);
					// Create new list
					jsonlistProject = new ArrayList<HashMap<String, String>>();

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

						jsonlistProject.add(proList);
						System.out.println("JSON PROPERTY LIST");
						System.out.println(jsonlistProject);
					}
					// Spinner set Array Data in Drop down

					sAdapPROJ = new SimpleAdapter(PropertyActivity.this, jsonlistProject, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyPROJECT.setAdapter(sAdapPROJ);

					for (int j = 0; j < jsonlistProject.size(); j++) {
						if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
							spinnerpropertyPROJECT.setSelection(j);
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

				System.out.println("Property Dropdown Details parse Response");
				System.out.println(arg0);
				return null;
			}
		});

	}

	protected void webRequestPropertyCaseList() {
		// CaseList parameters
		/*
		 * [ { "CaseFileNo": "1500000006", "RelatedFileNo": "", "BranchCode":
		 * "", "FileOpenedDate": "8/1/2015 12:00:00 AM", "IC": "3", "CaseType":
		 * "SPA", "ClientName": "", "BankName": "", "Branch": "", "LOTNo": "",
		 * "CaseAmount": "", "UserCode": "", "Status": "OPEN", "FileClosedDate":
		 * "" } ]
		 */

		try {

			// Passing value in JSON format in first 8-fields
			JSONObject jsonObject = new JSONObject();

			jsonObject.put("PropertyCode", titleNoDetailResponse);
			jsonObject.put("RelatedPartyCode", "");
			jsonObject.put("CallFrom", "PROPERTY");
			jsonObject.put("Category", "SPA");

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_PROPERTY_RELATED_CASELIST, params, new BaseJsonHttpResponseHandler<String>() {

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
							 * [ { "CaseFileNo": "1500000006", "RelatedFileNo":
							 * "", "BranchCode": "", "FileOpenedDate":
							 * "8/1/2015 12:00:00 AM", "IC": "3", "CaseType":
							 * "SPA", "ClientName": "", "BankName": "",
							 * "Branch": "", "LOTNo": "", "CaseAmount": "",
							 * "UserCode": "", "Status": "OPEN",
							 * "FileClosedDate": "" } ]
							 */

							CaseList_CaseFileNo = jsonResponse.getString("CaseFileNo").toString();
							CaseList_RelatedFileNo = jsonResponse.getString("RelatedFileNo").toString();
							CaseList_BranchCode = jsonResponse.getString("BranchCode").toString();
							CaseList_FileOpenedDate = jsonResponse.getString("FileOpenedDate").toString();
							CaseList_IC = jsonResponse.getString("IC").toString();
							CaseList_CaseType = jsonResponse.getString("CaseType").toString();
							CaseList_ClientName = jsonResponse.getString("ClientName").toString();
							CaseList_BankName = jsonResponse.getString("BankName").toString();
							// CaseList_Branch =
							// jsonResponse.getString("Branch").toString();
							CaseList_LOTNo = jsonResponse.getString("LOTNo").toString();
							CaseList_CaseAmount = jsonResponse.getString("CaseAmount").toString();
							CaseList_UserCode = jsonResponse.getString("UserCode").toString();
							CaseList_Status = jsonResponse.getString("Status").toString();
							CaseList_FileClosedDate = jsonResponse.getString("FileClosedDate").toString();

							// SEND JSON DATA INTO CASELIST
							HashMap<String, String> caseListProperty = new HashMap<String, String>();

							// Send JSON Data to list activity
							System.out.println("SEND JSON CASE LIST");

							caseListProperty.put("CaseFileNo_List", CaseList_CaseFileNo);
							System.out.println(CaseList_CaseFileNo);
							caseListProperty.put("RelatedFileNo_List", CaseList_RelatedFileNo);
							System.out.println(CaseList_RelatedFileNo);
							caseListProperty.put("BranchCode_List", CaseList_BranchCode);
							System.out.println(CaseList_BranchCode);
							caseListProperty.put("FileOpenedDate_List", CaseList_FileOpenedDate);
							System.out.println(CaseList_FileOpenedDate);
							caseListProperty.put("IC_List", CaseList_IC);
							System.out.println(CaseList_IC);
							caseListProperty.put("CaseType_List", CaseList_CaseType);
							System.out.println(CaseList_CaseType);
							caseListProperty.put("ClientName_List", CaseList_ClientName);
							System.out.println(CaseList_ClientName);
							caseListProperty.put("BankName_List", CaseList_BankName);
							System.out.println(CaseList_BankName);
							// caseListProperty.put("Branch_List",CaseList_Branch);
							// System.out.println(CaseList_Branch);
							caseListProperty.put("LOTNo_List", CaseList_LOTNo);
							System.out.println(CaseList_LOTNo);
							caseListProperty.put("CaseAmount_List", CaseList_CaseAmount);
							System.out.println(CaseList_CaseAmount);
							caseListProperty.put("UserCode_List", CaseList_UserCode);
							System.out.println(CaseList_UserCode);
							caseListProperty.put("Status_List", CaseList_Status);
							System.out.println(CaseList_Status);
							caseListProperty.put("FileClosedDate", CaseList_FileClosedDate);
							System.out.println(CaseList_FileClosedDate);
							System.out.println(" END SEND JSON CASE LIST");

							jsonCaselist.add(caseListProperty);
							System.out.println("JSON CASELIST");
							System.out.println(jsonCaselist);
						}

					} catch (JSONException e) { // TODO Auto-generated catc
												// block
						e.printStackTrace();
					}
					Toast.makeText(PropertyActivity.this, "Case Item Found", Toast.LENGTH_SHORT).show();
					Intent intentList = new Intent(PropertyActivity.this, PropertyRelatedCaseListActivity.class);
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

	protected void addDatapropertyDetails() {

		/*
		 * { "CODE": "P-005", "TITLETYPE": "SAMPLE", "TITLENO": "278902",
		 * "LOTTYPE": "Buildings Name", "LOTNO": "16302", "FORMERLY_KNOWN_AS":
		 * "Same", "BPM": "12Resi", "STATE": "Johor", "AREA": "Johor Bahru",
		 * "LOTAREA_SQM": "143", "LOTAREA_SQFT": "25", "LASTUPDATEDON": "",
		 * "DEVELOPER": "Ram", "DVLPR_CODE": "000000000001", "PROJECT": "",
		 * "DEVLICNO": "548", "DEVSOLICTOR": "Baha", "DVLPR_SOL_CODE":
		 * "000000000001", "DVLPR_LOC": "Ramiz", "LSTCHG_BANKCODE":
		 * "000000000001", "LSTCHG_BANKNAME": "INDUS", "LSTCHG_BRANCH": "KAMER",
		 * "LSTCHG_PANO": "124", "LSTCHG_PRSTNO": "TIEM21" }
		 */
		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		// jsonObject.put("Category", "SPA");
		try {
			// Find confirmation message
			// messageResult = jsonObject.getString("Result").toString();

			// jsonObject.put("CODE", "11");
			jsonObject.put("TITLETYPE", titleValue);
			jsonObject.put("TITLENO", propertytitleNo.getText().toString());
			jsonObject.put("LOTTYPE", propertyLotType.getText().toString());
			jsonObject.put("LOTNO", propertyLotPTDNo.getText().toString());
			jsonObject.put("FORMERLY_KNOWN_AS", propertyFormerlyKnownAs.getText().toString());
			jsonObject.put("BPM", propertyBandarPekanMukin.getText().toString());
			jsonObject.put("STATE", propertyDaerahState.getText().toString());
			jsonObject.put("AREA", propertyNageriArea.getText().toString());

			jsonObject.put("LOTAREA_SQM", propertyLOTAREA_SQM.getText().toString());
			jsonObject.put("LOTAREA_SQFT", propertyLOTAREA_SQFT.getText().toString());
			jsonObject.put("LASTUPDATEDON", propertyLASTUPDATEDON.getText().toString());
			jsonObject.put("DVLPR_CODE", developerValue_id);
			jsonObject.put("DEVELOPER", developerValue);
			jsonObject.put("PROJECT_CODE", projectValue_id);
			jsonObject.put("PROJECTNAME", projectValue);
			jsonObject.put("DEVLICNO", propertyDEVLICNO.getText().toString());
			jsonObject.put("DVLPR_SOL_CODE", solicitorValue_id);
			jsonObject.put("DEVSOLICTOR", solicitorValue);
			jsonObject.put("DVLPR_LOC", propertyDVLPR_LOC.getText().toString());

			jsonObject.put("LSTCHG_BANKCODE", bankValue_id);
			jsonObject.put("LSTCHG_BANKNAME", bankValue);
			jsonObject.put("LSTCHG_BRANCH", propertyLSTCHG_BRANCH.getText().toString());
			jsonObject.put("LSTCHG_PANO", propertyLSTCHG_PANO.getText().toString());
			jsonObject.put("LSTCHG_PRSTNO", propertyLSTCHG_PRSTNO.getText().toString());

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println("params");
			System.out.println(params);

			RestService.post(METHOD_ADD_PROPERTY, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Property Details Add Confirmed");
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
						Intent iAddBack = new Intent(context, PropertyActivity.class);
						startActivity(iAddBack);

						Toast.makeText(PropertyActivity.this, messageDisplay, Toast.LENGTH_SHORT).show();
					} else {
						Toast.makeText(PropertyActivity.this, messageDisplay, Toast.LENGTH_SHORT).show();

					}
				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {

					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println("Property Details Add Response");
					System.out.println(arg0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	protected void sendDataEditpropertyDetails() {

		/*
		 * { "CODE": "P-001", "TITLETYPE": "", "TITLENO": "278902", "LOTTYPE":
		 * "Buildings", "LOTNO": "16303", "FORMERLY_KNOWN_AS": "", "BPM": "",
		 * "STATE": "Johor", "AREA": "Johor Bahru", "LOTAREA_SQM": "143",
		 * "LOTAREA_SQFT": "", "LASTUPDATEDON": "", "DEVELOPER": "",
		 * "DVLPR_CODE": "", "PROJECT": "", "DEVLICNO": "", "DEVSOLICTOR": "",
		 * "DVLPR_SOL_CODE": "", "DVLPR_LOC": "", "LSTCHG_BANKCODE": "",
		 * "LSTCHG_BANKNAME": "", "LSTCHG_BRANCH": "", "LSTCHG_PANO": "",
		 * "LSTCHG_PRSTNO": "" }
		 */
		// Passing value in JSON format in first fields
		JSONObject jsonObject = new JSONObject();

		// jsonObject.put("Category", "SPA");
		try {
			// messageResult = jsonObject.getString("Result").toString();

			jsonObject.put("CODE", codeResonse_listview);
			jsonObject.put("TITLETYPE", titleValue);
			jsonObject.put("TITLENO", propertytitleNo.getText().toString());
			jsonObject.put("LOTTYPE", propertyLotType.getText().toString());
			jsonObject.put("LOTNO", propertyLotPTDNo.getText().toString());
			jsonObject.put("FORMERLY_KNOWN_AS", propertyFormerlyKnownAs.getText().toString());
			jsonObject.put("BPM", propertyBandarPekanMukin.getText().toString());
			jsonObject.put("STATE", propertyDaerahState.getText().toString());
			jsonObject.put("AREA", propertyNageriArea.getText().toString());

			jsonObject.put("LOTAREA_SQM", propertyLOTAREA_SQM.getText().toString());
			jsonObject.put("LOTAREA_SQFT", propertyLOTAREA_SQFT.getText().toString());
			jsonObject.put("LASTUPDATEDON", propertyLASTUPDATEDON.getText().toString());
			jsonObject.put("DVLPR_CODE", developerValue_id);
			jsonObject.put("DEVELOPER", developerValue);
			jsonObject.put("PROJECT_CODE", projectValue_id);
			jsonObject.put("PROJECTNAME", projectValue);
			jsonObject.put("DEVLICNO", propertyDEVLICNO.getText().toString());
			jsonObject.put("DVLPR_SOL_CODE", solicitorValue_id);
			jsonObject.put("DEVSOLICTOR", solicitorValue);
			jsonObject.put("DVLPR_LOC", propertyDVLPR_LOC.getText().toString());

			jsonObject.put("LSTCHG_BANKCODE", bankValue_id);
			jsonObject.put("LSTCHG_BANKNAME", bankValue);
			jsonObject.put("LSTCHG_BRANCH", propertyLSTCHG_BRANCH.getText().toString());
			jsonObject.put("LSTCHG_PANO", propertyLSTCHG_PANO.getText().toString());
			jsonObject.put("LSTCHG_PRSTNO", propertyLSTCHG_PRSTNO.getText().toString());
			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_EDIT_PROPERTY, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					System.out.println("Property Details Edited Confirmed");
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
						Intent iConfirmBack = new Intent(context, PropertyActivity.class);
						startActivity(iConfirmBack);
						Toast.makeText(PropertyActivity.this, messageDisplay, Toast.LENGTH_SHORT).show();
					} else {
						Toast.makeText(PropertyActivity.this, messageDisplay, Toast.LENGTH_SHORT).show();

					}

				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					// TODO Auto-generated method stub
					System.out.println("Property Details Edited Response");
					System.out.println(arg0);
					// Get Json response
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);
					return null;
				}
			});

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public void webRequestPropertyDetails() {

		try {

			// Passing value in JSON format in first 8-fields
			JSONObject jsonObject = new JSONObject();

			// jsonObject.put("Category", "SPA");
			if (titleValue.equals("-- Select --")) {
				titleValue = "";

			}
			jsonObject.put("TITLETYPE", titleValue);
			jsonObject.put("TITLENO", propertytitleNo.getText().toString());
			jsonObject.put("LOTTYPE", propertyLotType.getText().toString());
			jsonObject.put("LOT_NO", propertyLotPTDNo.getText().toString());
			jsonObject.put("FORMERLY_KNOWN_AS", propertyFormerlyKnownAs.getText().toString());
			jsonObject.put("BPM", propertyBandarPekanMukin.getText().toString());
			jsonObject.put("STATE", propertyDaerahState.getText().toString());
			jsonObject.put("AREA", propertyNageriArea.getText().toString());

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println(params);

			RestService.post(METHOD_LIST_PROPERTY, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					System.out.println("Property OnFailure");
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					System.out.println("Property OnSuccess");

					// Find Response for ListView
					try {

						arrayResponse = new JSONArray(arg2);
						jsonlist = new ArrayList<HashMap<String, String>>();
						for (int i = 0; i < arrayResponse.length(); i++) {
							jsonResponse = arrayResponse.getJSONObject(i);

							code = jsonResponse.getString("CODE").toString();
							titleTYPE = jsonResponse.getString("TITLETYPE").toString();
							titleNO = jsonResponse.getString("TITLENO").toString();
							lotTYPE = jsonResponse.getString("LOTTYPE").toString();
							lot_NO = jsonResponse.getString("LOT_NO").toString();
							formerly_KNOWN_AS = jsonResponse.getString("FORMERLY_KNOWN_AS").toString();
							bpm = jsonResponse.getString("BPM").toString();
							state = jsonResponse.getString("STATE").toString();
							area = jsonResponse.getString("AREA").toString();

							HashMap<String, String> intentListProperty = new HashMap<String, String>();
							// Send JSON Data to list activity
							System.out.println("SEND JSON DATA");
							intentListProperty.put("CODEsend", code);
							System.out.println(code);
							intentListProperty.put("TITLETYPEsend", titleTYPE);
							System.out.println(titleTYPE);
							intentListProperty.put("TITLENOsend", titleNO);
							System.out.println(titleNO);
							intentListProperty.put("LOTTYPEsend", lotTYPE);
							System.out.println(lotTYPE);
							intentListProperty.put("LOT_NOsend", lot_NO);
							System.out.println(lot_NO);
							intentListProperty.put("FORMERLY_KNOWN_ASsend", formerly_KNOWN_AS);
							System.out.println(formerly_KNOWN_AS);
							intentListProperty.put("BPMsend", bpm);
							System.out.println(bpm);
							intentListProperty.put("STATEsend", state);
							System.out.println(state);
							intentListProperty.put("AREAsend", area);
							System.out.println(area);
							System.out.println("SEND JSON DATA FOUND");

							jsonlist.add(intentListProperty);
							System.out.println("JSON LIST");
							System.out.println(jsonlist);
						}

					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}

					Toast.makeText(PropertyActivity.this, "Data Found", Toast.LENGTH_SHORT).show();
					Intent intentList = new Intent(PropertyActivity.this, PropertyListActivity.class);
					intentList.putExtra("ProjectJsonList", jsonlist);
					startActivity(intentList);

					System.out.println(arg2);
				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					System.out.println("Property OnParseResponse");
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
}
