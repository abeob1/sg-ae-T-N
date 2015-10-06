package com.abeo.tia.noordin;

import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.CursorLoader;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.ColorDrawable;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.PopupWindow;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemSelectedListener;

public class AddCaseStep3of4 extends BaseActivity implements OnClickListener {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_AddCase_ListOfItems
	// Find list of Add Case Item list web method
	
	// Passing value in JSON format in first fields
			
			 JSONArray arrOfJson = new JSONArray();
			 String Cropreturn;
	private final String METHOD_ADDCASE_ITEMLIST = "SPA_AddCase_ListOfItems";
	private final String METHOD_ADD_CASE3 = "SPA_AddCase_Purch_AddIndividual";
	
	
	// Get Project value fromapi
	private final String TitleType_DROPDOWN = "SPA_GetValidValues";
	private final String Gender_DROPDOWN = "SPA_GetValidValues";
	private final String addressToUse_DROPDOWN = "SPA_GetValidValues";
	private final String GetCorporateVal = "SPA_AddCase_GetCorporate";
	
	
	//spenner adaptor
	ArrayList<HashMap<String, String>> jsonlistProject = null, jsonlistProjectTitle = null, jsonlistBank = null,
			jsonlistDeveloper = null, jsonlistSolicitor = null;
	String id, name, id_b, name_b, id_d, name_d, id_s, name_s;
	SimpleAdapter sAdap = null, sAdapTYPE = null, sAdapPROJ = null, sAdapBANK = null, sAdapDEV = null,
			sAdapSOLIC = null;
	// Find spinner fields
	Spinner spinnerpropertyTitleType, spinnerpropertyGENDER, spinnerpropertyaddressToUse, spinnerpropertyDEVELOPER,
			spinnerpropertyDEVSOLICTOR;
	
	//textviews	
	TextView ID, TEXT, textBank_id, textDeveloper_id, textSolicitor_id, textTitle, textProject,
	textBank, textDeveloper, textSolicitor;
	
	//strings
	String titleValue_id = "", genderValue_id = "", addressValue_id = "", developerValue_id = "", solicitorValue_id = "",
			titleValue = "", genderValue = "", addressValue = "", developerValue = "", solicitorValue = "";
	String messageDisplay = "", StatusResult = "";
	
	//buttons
	Button btnClosePopup,btnCorporate,btnBackIc,btnFontIc,btnfind;
	
	String CARPORINDU = "INDIVIDUAL";
	
	

	EditText edittextFile;
	EditText EFullName, IDn1, IDn3, Taxno, mobile,Telephone,Office,idaddress1,idaddress2,idaddress3,idaddress4,idaddress5,
	comaddress1, comaddress2,comaddress3,comaddress4,comaddress5,last_update;
	Button buttonChooseDoc, buttonFileBrowser, buttonOk, buttonconfirm,buttonconfirm2;
	ListView listViewItem,Listviewarr;

	// Find Case list items
	String AddCaseListItemNo_detail = "", AddCaseList_ItemName_detail = "";

	TextView messageText;
	ProgressDialog dialog = null;

	/********** File Path *************/
	int serverResponseCode = 0;
	String upLoadServerUri = null;
	String selectedImagePath;
	final String uploadFilePath = Environment.getExternalStorageDirectory().getPath();
	final String uploadFileName = "/go.png";
	//selectedImagePath = "uploadFilePath"+"uploadFileName";

	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;
	ArrayList<HashMap<String, String>> jsonItemList = null;
	String itemCode = "", itemName = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_addcase_step3);
		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);
		Toast.makeText(AddCaseStep3of4.this, "reachec in AddCaseStep1of 4", Toast.LENGTH_SHORT).show();
		
		// Find the SharedPreferences Firstname
				SharedPreferences FirstName = getSharedPreferences("FirstName", Context.MODE_PRIVATE);		
				String FirName = FirstName.getString("FirstName", "");
				TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
				welcome.setText("Welcome "+FirName);

		// Find Mesaage Text and Edit field by Id
		messageText = (TextView) findViewById(R.id.messageText);
		edittextFile = (EditText) findViewById(R.id.editText_AddCaseStep1Browse);

		// Find By Id spinner Address To Use
		spinnerpropertyTitleType = (Spinner) findViewById(R.id.TitleType);
		spinnerpropertyGENDER = (Spinner) findViewById(R.id.gendersp);
		spinnerpropertyaddressToUse = (Spinner) findViewById(R.id.addresstoUsersp);
		
		
		// Spinner click listener
		spinnerpropertyTitleType.setOnItemSelectedListener(new OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
						ID = (TextView) view.findViewById(R.id.Id);
						titleValue_id = ID.getText().toString();
						TEXT = (TextView) view.findViewById(R.id.Name);
						titleValue = TEXT.getText().toString();

						// Showing selected spinner item
						//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
		// Spinner click listener
				
				
		// Spinner click listener
		spinnerpropertyGENDER.setOnItemSelectedListener(new OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
						ID = (TextView) view.findViewById(R.id.Id);
						genderValue_id = ID.getText().toString();
						TEXT = (TextView) view.findViewById(R.id.Name);
						genderValue = TEXT.getText().toString();

						// Showing selected spinner item
						//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
		// Spinner click listener
				
				
		// Spinner click listener
		spinnerpropertyaddressToUse.setOnItemSelectedListener(new OnItemSelectedListener() {

					@Override
					public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
						ID = (TextView) view.findViewById(R.id.Id);
						addressValue_id = ID.getText().toString();
						TEXT = (TextView) view.findViewById(R.id.Name);
						addressValue = TEXT.getText().toString();

						// Showing selected spinner item
						//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

					}

					@Override
					public void onNothingSelected(AdapterView<?> parent) {
						// TODO Auto-generated method stub

					}
				});
		// Spinner click listener
				
				
				
		// Find List View
		Listviewarr = (ListView) findViewById(R.id.listview_AddCaseStep3PurchaserList);		
		
		
		
		// Find EditText Fields
				EFullName = (EditText) findViewById(R.id.FullName);
				IDn1 = (EditText) findViewById(R.id.IDn1);
				IDn3 = (EditText) findViewById(R.id.IDn3);
				Taxno = (EditText) findViewById(R.id.Taxno);
				mobile = (EditText) findViewById(R.id.mobile);
				Telephone = (EditText) findViewById(R.id.Telephone);
				Office = (EditText) findViewById(R.id.Office);

				idaddress1 = (EditText) findViewById(R.id.idaddress1);
				idaddress2 = (EditText) findViewById(R.id.idaddress2);
				idaddress3 = (EditText) findViewById(R.id.idaddress3);
				idaddress4 = (EditText) findViewById(R.id.idaddress4);
				idaddress5 = (EditText) findViewById(R.id.idaddress5);
				comaddress1 = (EditText) findViewById(R.id.comaddress1);
				comaddress2 = (EditText) findViewById(R.id.comaddress2);
				comaddress3 = (EditText) findViewById(R.id.comaddress3);
				comaddress4 = (EditText) findViewById(R.id.comaddress4);
				comaddress5 = (EditText) findViewById(R.id.comaddress5);
				last_update = (EditText) findViewById(R.id.last_update);
		

		// Find button by Id
		
		buttonconfirm = (Button) findViewById(R.id.button_AddCaseStep3Confirm);
		//btnCorporate = (Button) findViewById(R.id.button_AddCaseStep3Corporate);
		//buttonChooseDoc = (Button) findViewById(R.id.button_AddCaseStep3Individual);
		
		btnFontIc = (Button) findViewById(R.id.button_AddCaseStep3FontIc);
		btnBackIc = (Button) findViewById(R.id.button_AddCaseStep3BackIc);
		btnfind  = (Button) findViewById(R.id.button_AddCaseStep3Find);
		

		
		//buttonChooseDoc.setOnClickListener(this);
		//dialog = ProgressDialog.show(AddCaseStep2of4.this, "", "Uploading file...", true);
		//addCaseListOfItems();
		
		//buttonFileBrowser.setOnClickListener(this);
		//buttonChooseDoc.setOnClickListener(this);
		buttonconfirm.setOnClickListener(this);
		//btnCorporate.setOnClickListener(this);
		btnBackIc.setOnClickListener(this);
		btnFontIc.setOnClickListener(this);
		btnfind.setOnClickListener(this);
		
		try {
			dropdownTitle();
			dropdownGender();
			dropdownaddressToUse();
			//disableAll();
			//EnableAll();
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}
	
	
	 
	private void EnableAll() {
		// TODO Auto-generated method stub
		
		System.out.println("Thom EnableAll");
		EFullName.setClickable(true);
		IDn1.setClickable(true);
		IDn3.setClickable(true);
		Taxno.setClickable(true);
		mobile.setClickable(true);
		Telephone.setClickable(true);
		Office.setClickable(true);
		idaddress1.setClickable(true);
		idaddress2.setClickable(true);
		idaddress3.setClickable(true);
		idaddress4.setClickable(true);
		idaddress5.setClickable(true);
		comaddress1.setClickable(true);
		comaddress2.setClickable(true);
		comaddress3.setClickable(true);
		comaddress4.setClickable(true);
		comaddress5.setClickable(true);
		
		EFullName.setEnabled(true);
		IDn1.setEnabled(true);
		IDn3.setEnabled(true);
		Taxno.setEnabled(true);
		mobile.setEnabled(true);
		Telephone.setEnabled(true);
		Office.setEnabled(true);
		idaddress1.setEnabled(true);
		idaddress2.setEnabled(true);
		idaddress3.setEnabled(true);
		idaddress4.setEnabled(true);
		idaddress5.setEnabled(true);
		comaddress1.setEnabled(true);
		comaddress2.setEnabled(true);
		comaddress3.setEnabled(true);
		comaddress4.setEnabled(true);
		comaddress5.setEnabled(true);
		
		spinnerpropertyTitleType.setEnabled(true);
		spinnerpropertyGENDER.setEnabled(true);
		spinnerpropertyaddressToUse.setEnabled(true);
		
		spinnerpropertyTitleType.setClickable(true);
		spinnerpropertyGENDER.setClickable(true);
		spinnerpropertyaddressToUse.setClickable(true);
		
	}

	private void disableAll() {
		System.out.println("Thom disableAll");
		EFullName.setEnabled(false);
		IDn1.setEnabled(false);
		IDn3.setEnabled(false);
		Taxno.setEnabled(false);
		mobile.setEnabled(false);
		Telephone.setEnabled(false);
		Office.setEnabled(false);
		idaddress1.setEnabled(false);
		idaddress2.setEnabled(false);
		idaddress3.setEnabled(false);
		idaddress4.setEnabled(false);
		idaddress5.setEnabled(false);
		comaddress1.setEnabled(false);
		comaddress2.setEnabled(false);
		comaddress3.setEnabled(false);
		comaddress4.setEnabled(false);
		comaddress5.setEnabled(false);
		spinnerpropertyTitleType.setEnabled(false);
		spinnerpropertyGENDER.setEnabled(false);
		spinnerpropertyaddressToUse.setEnabled(false);
		
		EFullName.setClickable(false);
		IDn1.setClickable(false);
		IDn3.setClickable(false);
		Taxno.setClickable(false);
		mobile.setClickable(false);
		Telephone.setClickable(false);
		Office.setClickable(false);
		idaddress1.setClickable(false);
		idaddress2.setClickable(false);
		idaddress3.setClickable(false);
		idaddress4.setClickable(false);
		idaddress5.setClickable(false);
		comaddress1.setClickable(false);
		comaddress2.setClickable(false);
		comaddress3.setClickable(false);
		comaddress4.setClickable(false);
		comaddress5.setClickable(false);
		spinnerpropertyTitleType.setClickable(false);
		spinnerpropertyGENDER.setClickable(false);
		spinnerpropertyaddressToUse.setClickable(false);
		
	}

	public void setAllEmty(){

		
		
		EFullName.setText("");
		IDn1.setText("");
		IDn3.setText("");
		Taxno.setText("");
		mobile.setText("");
		Telephone.setText("");
		Office.setText("");
		idaddress1.setText("");
		idaddress2.setText("");
		idaddress3.setText("");
		idaddress4.setText("");
		idaddress5.setText("");
		comaddress1.setText("");
		comaddress2.setText("");
		comaddress3.setText("");
		comaddress4.setText("");
		comaddress5.setText("");
		
		//spinnerpropertyTitleType.setEnabled(false);
		//spinnerpropertyGENDER.setEnabled(false);
		//spinnerpropertyaddressToUse.setEnabled(false);
		EnableAll();
	}

	@Override
	public void onClick(View v) {
		if (v == buttonChooseDoc) {
			System.out.println("BTN Clicked buttonChooseDoc");
			//btnconfirm();
			CARPORINDU = "INDIVIDUAL";
			setAllEmty();

		} else if (v == buttonFileBrowser) {
			
		} else if (v == buttonconfirm) {
			//dialog.dismiss();
			System.out.println("BTN Clicked Conform");
			btnconfirm();
			//Intent qIntent = new Intent(AddCaseStep3of4.this, AddCaseStep4of4.class);			
			//startActivity(qIntent);
		}
		else if(v==btnCorporate)
		{		
			initiatePopupWindow();
			System.out.println("Corp BTNClicked");
		}
		else if(v==btnFontIc)
		{		
			//initiatePopupWindow();
			System.out.println("Clicked btnFontIc");
		}
		else if(v==btnBackIc)
		{		
			//initiatePopupWindow();
			System.out.println("Clicked btnBackIc");
		}
		else if(v==btnfind)
		{
			initiatePopupfind();
		}
	}


	private void btnconfirm() {
		
		if(arrOfJson.length()==4)
		{
			AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AddCaseStep3of4.this);

			// set title
			alertDialogBuilder.setTitle("Confirm");

			// set dialog message
			alertDialogBuilder.setMessage("You Can't Add more Than 4 Items, Click ok To save!!!").setCancelable(false)
					.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
				public void onClick(DialogInterface dialog, int id) {

					
					sendDataEditpropertyDetails();
					dialog.cancel();

				}
			});
			// create alert dialog
			AlertDialog alertDialog = alertDialogBuilder.create();

			// show it
			alertDialog.show();
			
		}
		else
		{

			AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AddCaseStep3of4.this);

			// set title
			alertDialogBuilder.setTitle("Confirm");

			// set dialog message
			alertDialogBuilder.setMessage("Do you Want to Add More Purchaser ?").setCancelable(false)
					.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
				public void onClick(DialogInterface dialog, int id) {

					
					addDatapropertyDetails();
					setAllEmty();
					if(arrOfJson.length()==4)
						disableAll();
					dialog.cancel();
					

				}
			}).setNegativeButton("No", new DialogInterface.OnClickListener() {
				public void onClick(DialogInterface dialog, int id) {
					// if this button is clicked, just close
					// the dialog box and do nothing
						addDatapropertyDetails();
						sendDataEditpropertyDetails();				
					//Intent i = getIntent();
					//startActivity(i);
				}
			});
			// create alert dialog
			AlertDialog alertDialog = alertDialogBuilder.create();

			// show it
			alertDialog.show();
		}


		

	}
		
	public void addCaseListOfItems() {

		RequestParams params = null;
		params = new RequestParams();

		RestService.post(METHOD_ADDCASE_ITEMLIST, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Add Case List Item Success Details ");
				System.out.println(arg2);

				try {

					arrayResponse = new JSONArray(arg2);
					// Create new list
					jsonItemList = new ArrayList<HashMap<String, String>>();

					for (int i = 0; i < arrayResponse.length(); i++) {

						jsonResponse = arrayResponse.getJSONObject(i);

						itemCode = jsonResponse.getString("ItemCode").toString();
						itemName = jsonResponse.getString("ItemName").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> addCaseItemList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND ADD CASE ITEM LIST");
						addCaseItemList.put("ItemCode_T", itemCode);
						System.out.println(itemCode);
						addCaseItemList.put("ItemName_T", itemName);
						System.out.println(itemName);
						System.out.println(" END ADD CASE ITEM LIST");

						jsonItemList.add(addCaseItemList);
						System.out.println("JSON ADD CASE ITEM LIST");
						System.out.println(jsonItemList);
						
						
						
					}

				} catch (JSONException e) { // TODO Auto-generated
											// catch
											// block
					e.printStackTrace();
				}
				Toast.makeText(AddCaseStep3of4.this, "AddCase Item Found", Toast.LENGTH_SHORT).show();
				dialog.dismiss();
				// Simple Adapter for List
				SimpleAdapter simpleAdapter = new SimpleAdapter(AddCaseStep3of4.this, jsonItemList,
						R.layout.listview_column_addcase_itemlist, new String[] { "ItemCode_T", "ItemName_T" },
						new int[] { R.id.listAddCaseHeader_ItemCodeText, R.id.listAddCaseHeader_ItemNameText });

				listViewItem.setAdapter(simpleAdapter);
				
			

				listViewItem.setOnItemClickListener(new AdapterView.OnItemClickListener() {
					View ls=null;
					@Override
					public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
						
						
						if(get()!=view && get()!=null)
						{
							get().setActivated(false);
						}
						Toast.makeText(AddCaseStep3of4.this, "You Clicked at " + jsonItemList.get(position),
								Toast.LENGTH_SHORT).show();
						System.out.println(position);

						//int mSelectedItem = position;		
						 	
						 view.setActivated(true);
						 
						// Get list of Item data
						TextView c = (TextView) view.findViewById(R.id.listAddCaseHeader_ItemCodeText);
						itemCode = c.getText().toString();
						System.out.println(itemCode);
						String data = (String) parent.getItemAtPosition(position).toString();
						System.out.println(data);
						set(view);

					}

					private void set(View view) {						
						ls=view;						
					}
					private View get() {									
						return ls;
					}
				});

			}

			
			
			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {

				// Get Json response
				arrayResponse = new JSONArray(arg0);
				jsonResponse = arrayResponse.getJSONObject(0);

				System.out.println("Add Case List Itemparse ParseResponse");
				System.out.println(arg0);
				return null;
			}
		});

	}
	
	public void setallvalues(Object data) throws JSONException {

		Toast.makeText(AddCaseStep3of4.this, "You Clicked at " + data,
				Toast.LENGTH_SHORT).show();
		JSONObject jObj =  new JSONObject(data.toString());
		//JSONObject jObj = arr.getJSONObject(0);
		System.out.println(jObj);
		
		EFullName.setText(jObj.getString("EmployeeName"));
		IDn1.setText(jObj.getString("IDNo1"));
		IDn3.setText(jObj.getString("IDNo3"));
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
		comaddress5.setText(jObj.getString("CorresAddr5"));
		
		//spinnerpropertyTitleType.setEnabled(false);
		//spinnerpropertyGENDER.setEnabled(false);
		//spinnerpropertyaddressToUse.setEnabled(false);
		
		CARPORINDU = "CORPORATE";
		pwindo.setOutsideTouchable(true);
		pwindo.dismiss();
		disableAll();
	}

	protected void addDatapropertyDetails() {

		/*
		 * {
    "Code": "",
    "DocEntry": "",
    "EmployeeName": "Naveen",
    "Title": "SA",
    "Gender": "LELAKI",
    "IDNo1": "484424-08-5193",
    "IDNo3": "7584239",
    "TaxNo": "5541",
    "MobileNo": "7667266203",
    "Telephone": "",
    "OfficeNo": "",
    "IDAddress1": "47",
    "IDAddress2": "raja st",
    "IDAddress3": "kavin road",
    "IDAddress4": "chennai",
    "IDAddress5": "tamilnadu",
    "CorresAddr1": "44",
    "CorresAddr2": "ram nagar",
    "CorresAddr3": "ECR tail",
    "CorresAddr4": "chennai",
    "CorresAddr5": "tamilnadu",
    "AddressToUse": "ADDRESS_ID",
    "LastUpdatedOn": ""
}
		 */
		
		JSONObject jsonObject = new JSONObject();
		// jsonObject.put("Category", "SPA");
		try {
			// Find confirmation message
			// messageResult = jsonObject.getString("Result").toString();

			jsonObject.put("CardCode", "11");
			jsonObject.put("Code", "11");
			jsonObject.put("DocEntry", "");
			jsonObject.put("IDType", CARPORINDU);			
			jsonObject.put("EmployeeName", EFullName.getText().toString());
			jsonObject.put("Title", titleValue_id);
			jsonObject.put("Gender", genderValue_id);
			jsonObject.put("IDNo1", IDn1.getText().toString());
			jsonObject.put("IDNo3", IDn3.getText().toString());
			jsonObject.put("TaxNo", Taxno.getText().toString());

			jsonObject.put("MobileNo", mobile.getText().toString());
			jsonObject.put("Telephone", Telephone.getText().toString());
			jsonObject.put("OfficeNo", Office.getText().toString());
			jsonObject.put("IDAddress1", idaddress1.getText().toString());
			jsonObject.put("IDAddress2", idaddress2.getText().toString());
			jsonObject.put("IDAddress3", idaddress3.getText().toString());
			jsonObject.put("IDAddress4", idaddress4.getText().toString());
			jsonObject.put("IDAddress5", idaddress5.getText().toString());
			jsonObject.put("CorresAddr1", comaddress1.getText().toString());
			jsonObject.put("CorresAddr2", comaddress2.getText().toString());
			jsonObject.put("CorresAddr3", comaddress3.getText().toString());

			jsonObject.put("CorresAddr4", comaddress4.getText().toString());
			jsonObject.put("CorresAddr5", comaddress5.getText().toString());
			jsonObject.put("AddressToUse", addressValue_id);
			jsonObject.put("LastUpdatedOn", last_update.getText().toString());

			
			
			arrOfJson.put(jsonObject);
			System.out.println("JsonArray:"+arrOfJson.toString());
			
		    upDateList();

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	private void upDateList() {
		// TODO Auto-generated method stub
		
		
		try {

			arrayResponse = arrOfJson;
			// Create new list
			jsonItemList = new ArrayList<HashMap<String, String>>();
			 
						
			for (int i = 0; i < arrayResponse.length(); i++) {
				
				//JSONObject leagueData = arrayResponse.getJSONObject(i);

				jsonResponse = arrayResponse.getJSONObject(i);

				itemCode = jsonResponse.getString("EmployeeName").toString();
				itemName = jsonResponse.getString("MobileNo").toString();

				// SEND JSON DATA INTO SPINNER TITLE LIST
				HashMap<String, String> addCaseItemList = new HashMap<String, String>();

				// Send JSON Data to list activity
				System.out.println("SEND ADD CASE ITEM LIST");
				addCaseItemList.put("ItemCode_T", itemCode);
				System.out.println(itemCode);
				addCaseItemList.put("ItemName_T", itemName);
				System.out.println(itemName);
				System.out.println(" END ADD CASE ITEM LIST");

				jsonItemList.add(addCaseItemList);
				System.out.println("JSON ADD CASE ITEM LIST");
				System.out.println(jsonItemList);
				
				
				// Simple Adapter for List
				SimpleAdapter simpleAdapter = new SimpleAdapter(AddCaseStep3of4.this, jsonItemList,
						R.layout.listview_column_addcase_itemlist, new String[] { "ItemCode_T", "ItemName_T" },
						new int[] { R.id.listAddCaseHeader_ItemCodeText, R.id.listAddCaseHeader_ItemNameText });

				Listviewarr.setAdapter(simpleAdapter);
				
				
			}

		} catch (JSONException e) { // TODO Auto-generated
									// catch
									// block
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
		//JSONObject jsonObject = new JSONObject();

		// messageResult = jsonObject.getString("Result").toString();
		RequestParams params = new RequestParams();
		params.put("sJsonInput", arrOfJson.toString());
		System.out.println("params");
		System.out.println(params);

		RestService.post(METHOD_ADD_CASE3, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
				System.out.println("onFailure");

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("onFailure");
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
					
					Intent qIntent = new Intent(AddCaseStep3of4.this, AddCaseStep4of4.class);
					Toast.makeText(AddCaseStep3of4.this, messageDisplay, Toast.LENGTH_SHORT).show();
					startActivity(qIntent);
					
					
					
					
				} else {
					Toast.makeText(AddCaseStep3of4.this, messageDisplay, Toast.LENGTH_SHORT).show();

				}
			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {

				// Get Json response
				arrayResponse = new JSONArray(arg0);
				jsonResponse = arrayResponse.getJSONObject(0);

				System.out.println("Addcase3 Details Add Response");
				System.out.println(arg0);
				return null;
			}
		});

	}

	public void dropdownTitle() throws JSONException {

		RequestParams params = null;
		params = new RequestParams();
		
		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "@AE_RELATEDPARTY");
		jsonObject.put("FieldName", "INDIVIDUAL_TITLE");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(TitleType_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
				System.out.println("Failed");

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Title Dropdown Success Details ");
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

					sAdapPROJ = new SimpleAdapter(AddCaseStep3of4.this, jsonlistProject, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyTitleType.setAdapter(sAdapPROJ);

					/*for (int j = 0; j < jsonlistProject.size(); j++) {
						if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
							TitleType_DROPDOWN.setSelection(j);
							break;
						}
					}*/

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

	public void dropdownGender() throws JSONException {

		RequestParams params = null;
		params = new RequestParams();
		
		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "@AE_RELATEDPARTY");
		jsonObject.put("FieldName", "GENDER");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(Gender_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
				System.out.println("Failed");

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Gender Dropdown Success Details ");
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

					sAdapPROJ = new SimpleAdapter(AddCaseStep3of4.this, jsonlistProject, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyGENDER.setAdapter(sAdapPROJ);

					/*for (int j = 0; j < jsonlistProject.size(); j++) {
						if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
							TitleType_DROPDOWN.setSelection(j);
							break;
						}
					}*/

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

				System.out.println("Gender Dropdown Details parse Response");
				System.out.println(arg0);
				return null;
			}
		});

	}

	public void dropdownaddressToUse() throws JSONException {

		RequestParams params = null;
		params = new RequestParams();
		
		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "@AE_RELATEDPARTY");
		jsonObject.put("FieldName", "ADDRESS_TOUSE");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(addressToUse_DROPDOWN, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
				System.out.println("Failed");

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Address To Use Dropdown Success Details ");
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

					sAdapPROJ = new SimpleAdapter(AddCaseStep3of4.this, jsonlistProject, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyaddressToUse.setAdapter(sAdapPROJ);

					/*for (int j = 0; j < jsonlistProject.size(); j++) {
						if (jsonlistProject.get(j).get("Id_T").equals(projectDetailResponse)) {
							TitleType_DROPDOWN.setSelection(j);
							break;
						}
					}*/

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

				System.out.println("addressToUse Dropdown Details parse Response");
				System.out.println(arg0);
				return null;
			}
		});

	}


	private PopupWindow pwindo;
	
	
	private void initiatePopupWindow() {
		final ListView listview_corp;
		try {
		// We need to get the instance of the LayoutInflater
		LayoutInflater inflater = (LayoutInflater) AddCaseStep3of4.this	.getSystemService(getApplicationContext().LAYOUT_INFLATER_SERVICE);
		View layout = inflater.inflate(R.layout.corppopup,(ViewGroup) findViewById(R.id.popup_element),false);
		pwindo = new PopupWindow(layout, 800, 870, true);
		pwindo.setOutsideTouchable(true);
		pwindo.showAtLocation(layout, Gravity.CENTER, 0, 0);
		System.out.println("PopUp Inovaked");

		btnClosePopup = (Button) layout.findViewById(R.id.btn_close_popup);
		btnClosePopup.setOnClickListener(cancel_button_click_listener);
		buttonconfirm2  = (Button) layout.findViewById(R.id.button_PropertyConfirm);
		listview_corp = (ListView) layout.findViewById(R.id.listview_corp);
		
		
		listview_corp.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			View ls=null;
			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				
				
				if(get()!=view && get()!=null)
				{
					get().setActivated(false);
				}
				try {
					setallvalues(arrayResponse.get(position));
					
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				System.out.println(position);

				//int mSelectedItem = position;		
				 	
				 view.setActivated(true);
				 
				/* Get list of Item data
				TextView c = (TextView) view.findViewById(R.id.listAddCaseHeader_ItemCodeText);
				itemCode = c.getText().toString();
				System.out.println(itemCode);
				String data = (String) parent.getItemAtPosition(position).toString();
				System.out.println(data);*/
				set(view);

			}

			private void set(View view) {						
				ls=view;						
			}
			private View get() {									
				return ls;
			}
		});
		
		//getcrop

		RequestParams params = null;
		params = new RequestParams();
		
		final String returnarg;

		RestService.post(GetCorporateVal, params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
				System.out.println("Failed");

			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("GetCorporate Success Details ");
				System.out.println(arg2);
				
				try {
					arrayResponse = new JSONArray(arg2);
				
				// Create new list
				jsonItemList = new ArrayList<HashMap<String, String>>();
				 
				System.out.println("PopUp Inovaked");
				for (int i = 0; i < arrayResponse.length(); i++) {
					
					//JSONObject leagueData = arrayResponse.getJSONObject(i);

					jsonResponse = arrayResponse.getJSONObject(i);

					itemCode = jsonResponse.getString("Code").toString();
					itemName = jsonResponse.getString("EmployeeName").toString();

					// SEND JSON DATA INTO SPINNER TITLE LIST
					HashMap<String, String> addCaseItemList = new HashMap<String, String>();

					// Send JSON Data to list activity
					System.out.println("SEND ADD CASE ITEM LIST");
					addCaseItemList.put("ItemCode_T", itemCode);
					System.out.println(itemCode);
					addCaseItemList.put("ItemName_T", itemName);
					System.out.println(itemName);
					System.out.println(" END ADD CASE ITEM LIST");

					jsonItemList.add(addCaseItemList);
					System.out.println("JSON ADD CASE ITEM LIST");
					System.out.println(jsonItemList);	
					
				}
				
				// Simple Adapter for List
				SimpleAdapter simpleAdapter = new SimpleAdapter(AddCaseStep3of4.this, jsonItemList,
						R.layout.listview_column_addcase_itemlist, new String[] { "ItemCode_T", "ItemName_T" },
						new int[] { R.id.listAddCaseHeader_ItemCodeText, R.id.listAddCaseHeader_ItemNameText });

				listview_corp.setAdapter(simpleAdapter);
				
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				};

			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {

				// Get Json response
				arrayResponse = new JSONArray(arg0);
				jsonResponse = arrayResponse.getJSONObject(0);

				System.out.println("Corprate Dropdown Details parse Response");
				System.out.println(arg0);
				return null;
			}		
		});

		} catch (Exception e) {
		e.printStackTrace();
		}
		}
	
private void initiatePopupfind() {
		
		try {
		// We need to get the instance of the LayoutInflater
		LayoutInflater inflater = (LayoutInflater) AddCaseStep3of4.this	.getSystemService(getApplicationContext().LAYOUT_INFLATER_SERVICE);
		View layout = inflater.inflate(R.layout.findbuttons,(ViewGroup) findViewById(R.id.popup_element),false);
		pwindo = new PopupWindow(layout, 150, 100, true);
		pwindo.setOutsideTouchable(true);
		pwindo.showAtLocation(layout, Gravity.TOP, 190, 120);
		pwindo.setBackgroundDrawable(new ColorDrawable(
	            android.graphics.Color.TRANSPARENT));
		System.out.println("PopUp Inovaked");

		btnClosePopup = (Button) layout.findViewById(R.id.button_AddCaseStep3Individual);
		btnClosePopup.setOnClickListener(Individual_click_listener);
		buttonconfirm2  = (Button) layout.findViewById(R.id.button_AddCaseStep3Corporate);
		buttonconfirm2.setOnClickListener(Corporate_click_listener);
		
		
		
		//initiatePopupWindow();
		} catch (Exception e) {
		e.printStackTrace();
		}
		}
	
	
	private OnClickListener Individual_click_listener = new OnClickListener() {
		public void onClick(View v) {
		pwindo.dismiss();
		pwindo.isOutsideTouchable();
		CARPORINDU = "INDIVIDUAL";
		setAllEmty();

		}
		};
		
	private OnClickListener Corporate_click_listener = new OnClickListener() {
		public void onClick(View v) {
		pwindo.dismiss();
		pwindo.isOutsideTouchable();
		initiatePopupWindow();

		}
		};
		
	
	
	private OnClickListener cancel_button_click_listener = new OnClickListener() {
		public void onClick(View v) {
		pwindo.dismiss();
		pwindo.isOutsideTouchable();

		}
		};

		
}
