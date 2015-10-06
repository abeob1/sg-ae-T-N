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
import android.content.Intent;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class PropertyRelatedCaseListActivity extends BaseActivity {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_PropertyEnquiryDetails

	private final String METHOD_NAME_PROPERTY_LISTOFCASE = "SPA_ListOfCases";

	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	ListView listView_Property;

	TextView listCaseHeader_codeTextView, listCaseHader_caseFileNoTextView, listCaseHeader_relatedFileNoTextView,
			listCaseHeader_branchCodeTextView, listCaseHeader_FileOpendDateTextView, listCaseHeader_iCTextView,
			listCaseHeader_caseTypeTextView, listCaseHeader_clientNameTextView, listCaseHeader_BankTextView,
			listCaseHeader_lotPTDPTNoTextView, listCaseHeader_caseAmountTextView, listCaseHeader_userCodeTextView,
			listCaseHeader_statusTextView, listCaseHeader_fileClosedDateTextView, TextView;
	// Find Case list items
	String CaseList_CaseFileNo_detail = "", CaseList_RelatedFileNo_detail = "", CaseList_BranchCode_detail = "",
			CaseList_FileOpenedDate_detail = "", CaseList_IC_detail = "", CaseList_CaseType_detail = "",
			CaseList_ClientName_detail = "", CaseList_BankName_detail = "", CaseList_Branch_detail = "",
			CaseList_LOTNo_detail = "", CaseList_CaseAmount_detail = "", CaseList_UserCode_detail = "",
			CaseList_Status_detail = "", CaseList_FileClosedDate_detail = "";

	JSONArray arrayResponse;
	JSONObject jsonResponse;
	String codeData = "";
	ArrayList<HashMap<String, String>> myArrayCaseList;

	@SuppressWarnings("unchecked")
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_caselistview_proprty);

		// Find Set Function null object reference
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items); // load
		// titles
		// from
		// strings.xml

		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);// load
		// icons
		// from
		// strings.xml
		set(navMenuTitles, navMenuIcons);

		// Find ListView
		listView_Property = (ListView) findViewById(R.id.listview_RelatedCaseList);

		// Find TextView
		listCaseHeader_codeTextView = (TextView) findViewById(R.id.listCaseHeader_codeText);
		listCaseHader_caseFileNoTextView = (TextView) findViewById(R.id.listCaseHader_caseFileNoText);
		listCaseHeader_relatedFileNoTextView = (TextView) findViewById(R.id.listCaseHeader_relatedFileNoText);
		listCaseHeader_branchCodeTextView = (TextView) findViewById(R.id.listCaseHeader_branchCodeText);
		listCaseHeader_FileOpendDateTextView = (TextView) findViewById(R.id.listCaseHeader_FileOpendDateText);
		listCaseHeader_iCTextView = (TextView) findViewById(R.id.listCaseHeader_iCText);
		listCaseHeader_caseTypeTextView = (TextView) findViewById(R.id.listCaseHeader_caseTypeText);
		listCaseHeader_clientNameTextView = (TextView) findViewById(R.id.listCaseHeader_clientNameText);
		listCaseHeader_BankTextView = (TextView) findViewById(R.id.listCaseHeader_BankText);
		listCaseHeader_lotPTDPTNoTextView = (TextView) findViewById(R.id.listCaseHeader_lotPTDPTNoText);
		listCaseHeader_caseAmountTextView = (TextView) findViewById(R.id.listCaseHeader_caseAmountText);
		listCaseHeader_userCodeTextView = (TextView) findViewById(R.id.listCaseHeader_userCodeText);
		listCaseHeader_statusTextView = (TextView) findViewById(R.id.listCaseHeader_statusText);
		listCaseHeader_fileClosedDateTextView = (TextView) findViewById(R.id.listCaseHeader_fileClosedDateText);

		myArrayCaseList = (ArrayList<HashMap<String, String>>) getIntent().getSerializableExtra("ProjectJsonList");

		// Adapter
		SimpleAdapter simpleAdapter = new SimpleAdapter(PropertyRelatedCaseListActivity.this, myArrayCaseList,
				R.layout.listviewcase_column_property,
				new String[] { "CODE_List","CaseFileNo_List", "RelatedFileNo_List", "BranchCode_List", "FileOpenedDate_List",
						"IC_List", "CaseType_List", "ClientName_List", "BankName_List", "LOTNo_List", "CaseAmount_List",
						"UserCode_List", "Status_List", "FileClosedDate" },
				new int[] { R.id.listCaseHeader_codeText, R.id.listCaseHader_caseFileNoText,
						R.id.listCaseHeader_relatedFileNoText, R.id.listCaseHeader_branchCodeText,
						R.id.listCaseHeader_FileOpendDateText, R.id.listCaseHeader_iCText,
						R.id.listCaseHeader_caseTypeText, R.id.listCaseHeader_clientNameText,
						R.id.listCaseHeader_BankText, R.id.listCaseHeader_lotPTDPTNoText,
						R.id.listCaseHeader_caseAmountText, R.id.listCaseHeader_userCodeText,
						R.id.listCaseHeader_statusText, R.id.listCaseHeader_fileClosedDateText });

		listView_Property.setAdapter(simpleAdapter);

		listView_Property.setOnItemClickListener(new AdapterView.OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				Toast.makeText(PropertyRelatedCaseListActivity.this, "You Clicked at " + myArrayCaseList.get(position),
						Toast.LENGTH_SHORT).show();
				System.out.println(position);

				// Get list of Item data
				TextView c = (TextView) view.findViewById(R.id.listCaseHeader_codeText);
				codeData = c.getText().toString();
				System.out.println(codeData);
				String data = (String) parent.getItemAtPosition(position).toString();
				System.out.println(data);

			}
		});

	}

}
