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

public class PropertyListActivity extends BaseActivity {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_PropertyEnquiryDetails

	private final String METHOD_NAME_PROPERTY_DETAILS = "SPA_PropertyEnquiryDetails";

	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	ListView listView_Property;

	TextView codeTextView, titleTypeTextView, titleNoTextView, lotTypeText, lotNoTextView, formerlyKnownTextView,
			bpmTextView, stateTextView, areaTextview;
	String CODE_detail = "", TITLETYPE_detail = "", TITLENO_detail = "", LOTTYPE_detail = "", LOTNO_detail = "",
			FORMERLY_KNOWN_AS_detail = "", BPM_detail = "", STATE_detail = " ", AREA_detail = "",
			LOTAREA_SQM_details = "", LOTAREA_SQFT_detail = "", LASTUPDATEDON_detail = "", DEVELOPER_detail = "",
			DVLPR_CODE_detail = "", PROJECT_detail = "", DEVLICNO_detail = "", DEVSOLICTOR_detail = "",
			DVLPR_SOL_CODE_detail = "", DVLPR_LOC_detail = "", LSTCHG_BANKCODE_detail = "", LSTCHG_BANKNAME_detail = "",
			LSTCHG_BRANCH_detail = "", LSTCHG_PANO_detail = "", LSTCHG_PRSTNO_detail = "";

	JSONArray arrayResponse;
	JSONObject jsonResponse;
	String codeData = "";
	ArrayList<HashMap<String, String>> myArrayList;

	@SuppressWarnings("unchecked")
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_listview_property);

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
		listView_Property = (ListView) findViewById(R.id.listview_property);

		// Find TextView
		codeTextView = (TextView) findViewById(R.id.listProprtyHeader_code);
		titleTypeTextView = (TextView) findViewById(R.id.listProprtyHeader_titleTypeText);
		titleNoTextView = (TextView) findViewById(R.id.listProprtyHeader_titleNoText);
		lotTypeText = (TextView) findViewById(R.id.listProprtyHeader_lotTypeText);
		lotNoTextView = (TextView) findViewById(R.id.listProprtyHeader_lotPtdPtNoText);
		formerlyKnownTextView = (TextView) findViewById(R.id.listProprtyHeader_formerlyKnownAsText);
		bpmTextView = (TextView) findViewById(R.id.listProprtyHeader_bandarPekanMukinText);
		stateTextView = (TextView) findViewById(R.id.listProprtyHeader_daerahText);
		areaTextview = (TextView) findViewById(R.id.listProprtyHeader_negeriText);

		myArrayList = (ArrayList<HashMap<String, String>>) getIntent().getSerializableExtra("ProjectJsonList");

		// Adapter
		SimpleAdapter simpleAdapter = new SimpleAdapter(PropertyListActivity.this, myArrayList,
				R.layout.listview_column_property,
				new String[] { "CODEsend", "TITLETYPEsend", "TITLENOsend", "LOTTYPEsend", "LOT_NOsend",
						"FORMERLY_KNOWN_ASsend", "BPMsend", "STATEsend", "AREAsend" },
				new int[] { R.id.listProprtyHeader_code, R.id.listProprtyHeader_titleTypeText,
						R.id.listProprtyHeader_titleNoText, R.id.listProprtyHeader_lotTypeText,
						R.id.listProprtyHeader_lotPtdPtNoText, R.id.listProprtyHeader_formerlyKnownAsText,
						R.id.listProprtyHeader_bandarPekanMukinText, R.id.listProprtyHeader_daerahText,
						R.id.listProprtyHeader_negeriText });

		listView_Property.setAdapter(simpleAdapter);

		listView_Property.setOnItemClickListener(new AdapterView.OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				Toast.makeText(PropertyListActivity.this, "You Clicked at " + myArrayList.get(position),
						Toast.LENGTH_SHORT).show();
				System.out.println(position);
				// Intent myIntent = new Intent(view.getContext(),
				// PropertyActivity.class);
				// startActivity(myIntent);

				// Get list of Item data
				TextView c = (TextView) view.findViewById(R.id.listProprtyHeader_code);
				codeData = c.getText().toString();
				System.out.println(codeData);
				String data = (String) parent.getItemAtPosition(position).toString();
				System.out.println(data);

				// Call function for web service SPA_PropertyEnquiryDetails
				getListPropertydetails();

			}
		});

	}

	protected void getListPropertydetails() {

		JSONObject jsonObject = new JSONObject();
		// { "Code": "P-001", "Category": "SPA" }
		try {
			jsonObject.put("Code", codeData);
			jsonObject.put("Category", "SPA");

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());

			RestService.post(METHOD_NAME_PROPERTY_DETAILS, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println("On Item Click OnFailure");
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("On Item Click On Success");
					System.out.println(arg2);

					try {
						CODE_detail = jsonResponse.getString("CODE").toString();
						TITLETYPE_detail = jsonResponse.getString("TITLETYPE").toString();
						TITLENO_detail = jsonResponse.getString("TITLENO").toString();
						LOTTYPE_detail = jsonResponse.getString("LOTTYPE").toString();
						LOTNO_detail = jsonResponse.getString("LOTNO").toString();

						FORMERLY_KNOWN_AS_detail = jsonResponse.getString("FORMERLY_KNOWN_AS").toString();
						BPM_detail = jsonResponse.getString("BPM").toString();
						STATE_detail = jsonResponse.getString("STATE").toString();
						AREA_detail = jsonResponse.getString("AREA").toString();

						LOTAREA_SQM_details = jsonResponse.getString("LOTAREA_SQM").toString();
						LOTAREA_SQFT_detail = jsonResponse.getString("LOTAREA_SQFT").toString();
						LASTUPDATEDON_detail = jsonResponse.getString("LASTUPDATEDON").toString();
						DEVELOPER_detail = jsonResponse.getString("DEVELOPER").toString();
						DVLPR_CODE_detail = jsonResponse.getString("DVLPR_CODE").toString();
						PROJECT_detail = jsonResponse.getString("PROJECTNAME").toString();
						DEVLICNO_detail = jsonResponse.getString("DEVLICNO").toString();
						DEVSOLICTOR_detail = jsonResponse.getString("DEVSOLICTOR").toString();
						DVLPR_SOL_CODE_detail = jsonResponse.getString("DVLPR_SOL_CODE").toString();
						DVLPR_LOC_detail = jsonResponse.getString("DVLPR_LOC").toString();
						LSTCHG_BANKCODE_detail = jsonResponse.getString("LSTCHG_BANKCODE").toString();
						LSTCHG_BANKNAME_detail = jsonResponse.getString("LSTCHG_BANKNAME").toString();
						LSTCHG_BRANCH_detail = jsonResponse.getString("LSTCHG_BRANCH").toString();
						LSTCHG_PANO_detail = jsonResponse.getString("LSTCHG_PANO").toString();
						LSTCHG_PRSTNO_detail = jsonResponse.getString("LSTCHG_PRSTNO").toString();
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}

					// Find Intent to call view property details
					Intent i = new Intent(PropertyListActivity.this, PropertyActivity.class);
					Toast.makeText(PropertyListActivity.this, "Item Clicked", Toast.LENGTH_SHORT).show();

					// Send the property details in property UI through intent

					i.putExtra("CODE_T", CODE_detail);
					System.out.println(" TITLETYPE_detail value");
					System.out.println(CODE_detail);

					i.putExtra("TITLETYPE_T", TITLETYPE_detail);
					System.out.println(TITLETYPE_detail);

					i.putExtra("TITLENO_T", TITLENO_detail);
					System.out.println(TITLENO_detail);

					i.putExtra("LOTTYPE_T", LOTTYPE_detail);
					System.out.println(LOTTYPE_detail);

					i.putExtra("LOTNO_T", LOTNO_detail);
					System.out.println(LOTNO_detail);

					i.putExtra("FORMERLY_KNOWN_AS_T", FORMERLY_KNOWN_AS_detail);
					System.out.println(FORMERLY_KNOWN_AS_detail);

					i.putExtra("BPM_T", BPM_detail);
					System.out.println(BPM_detail);

					i.putExtra("STATE_T", STATE_detail);
					System.out.println(STATE_detail);

					i.putExtra("AREA_T", AREA_detail);
					System.out.println(AREA_detail);

					i.putExtra("LOTAREA_SQM_T", LOTAREA_SQM_details);
					System.out.println(LOTAREA_SQM_details);

					i.putExtra("LOTAREA_SQFT_T", LOTAREA_SQFT_detail);
					System.out.println(LOTAREA_SQFT_detail);

					i.putExtra("LASTUPDATEDON_T", LASTUPDATEDON_detail);
					System.out.println(LASTUPDATEDON_detail);

					i.putExtra("DEVELOPER_T", DEVELOPER_detail);
					System.out.println(DEVELOPER_detail);
					
					i.putExtra("DVLPR_CODE_T", DVLPR_CODE_detail);
					System.out.println(DVLPR_CODE_detail);

					i.putExtra("PROJECT_T", PROJECT_detail);
					System.out.println(PROJECT_detail);

					i.putExtra("DEVLICNO_T", DEVLICNO_detail);
					System.out.println(DEVLICNO_detail);

					i.putExtra("DEVSOLICTOR_T", DEVSOLICTOR_detail);
					System.out.println(DEVSOLICTOR_detail);
					
					i.putExtra("DVLPR_SOL_CODE_T", DVLPR_SOL_CODE_detail);
					System.out.println(DVLPR_SOL_CODE_detail);

					i.putExtra("DVLPR_LOC_T", DVLPR_LOC_detail);
					System.out.println(DVLPR_LOC_detail);
					
					i.putExtra("LSTCHG_BANKCODE_T", LSTCHG_BANKCODE_detail);
					System.out.println(LSTCHG_BANKCODE_detail);

					i.putExtra("LSTCHG_BANKNAME_T", LSTCHG_BANKNAME_detail);
					System.out.println(LSTCHG_BANKNAME_detail);

					i.putExtra("LSTCHG_BRANCH_T", LSTCHG_BRANCH_detail);
					System.out.println(LSTCHG_BRANCH_detail);

					i.putExtra("LSTCHG_PANO_T", LSTCHG_PANO_detail);
					System.out.println(LSTCHG_PANO_detail);

					i.putExtra("LSTCHG_PRSTNO_T", LSTCHG_PRSTNO_detail);
					System.out.println(LSTCHG_PRSTNO_detail);

					startActivity(i);

				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					// TODO Auto-generated method stub
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);

					System.out.println("On Item Click Response");
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
