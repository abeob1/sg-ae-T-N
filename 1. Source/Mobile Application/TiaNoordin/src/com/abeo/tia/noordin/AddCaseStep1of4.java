package com.abeo.tia.noordin;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.HashMap;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;
import com.loopj.android.http.SyncHttpClient;
import com.loopj.android.http.TextHttpResponseHandler;

import abeo.tia.noordin.R;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.CursorLoader;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.provider.MediaStore;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

public class AddCaseStep1of4 extends BaseActivity implements OnClickListener {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_AddCase_ListOfItems
	// Find list of Add Case Item list web method
	private final String METHOD_ADDCASE_ITEMLIST = "SPA_AddCase_ListOfItems";
	private final String SPA_AddCase_DocumentToRead = "SPA_AddCase_DocumentToRead";
	
	private final String METHOD_ADDFILE = "http://54.251.51.69:3878/SPAMobile.asmx/Attachments";
	
	

	EditText edittextFile;
	Button buttonChooseDoc, buttonFileBrowser, buttonOk, buttonCancle;
	ListView listViewItem;
	String upLoadServerUri1 = null;
	
	int RESULTRES;
	// Find Case list items
	String AddCaseListItemNo_detail = "", AddCaseList_ItemName_detail = "", FILEUPLOADRESULT = "";

	TextView messageText;
	ProgressDialog dialog = null;

	/********** File Path *************/
	int serverResponseCode = 0;
	String upLoadServerUri = null;
	private static final int REQUEST_PICK_FILE = 1;


	private static final String METHOD_ADDCASE_DOCUMENT = "SPA_AddCase_DocumentToRead";
	String selectedImagePath,CardCodeResponse;
	private TextView filePath, msg, pdf;
	Button Browse, Convert,walkin;
	private File selectedFile;
	public String fileName;
	String encodedString = "";
	StringBuilder sb;
	
	
	
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
		setContentView(R.layout.activity_addcase_step1);
		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);
		//Toast.makeText(AddCaseStep1of4.this, "reachec in AddCaseStep1of 4", Toast.LENGTH_SHORT).show();
		
		
		upLoadServerUri1 = "http://54.251.51.69:3878/upload.php";
		
		
		// Find the SharedPreferences Firstname
				SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);		
				String FirName = FirstName.getString("FIRSETNAME", "");
				TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
				welcome.setText("Welcome "+FirName);
				
				// Find the SharedPreferences pass Login value
				SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
				System.out.println("LOGIN DATA");
				String userName = prefLoginReturn.getString("sUserName", "");
				
				String category = prefLoginReturn.getString("sCategory", "");
				System.out.println(category);
				String CardCode = prefLoginReturn.getString("CardCode", "");
				System.out.println(CardCode);
				CardCodeResponse = CardCode;

		// Find Mesaage Text and Edit field by Id
		messageText = (TextView) findViewById(R.id.messageText);
		edittextFile = (EditText) findViewById(R.id.editText_AddCaseStep1Browse);

		// Find List View
		listViewItem = (ListView) findViewById(R.id.listview_addCaseStep1Doc);

		// Find button by Id
		walkin = (Button) findViewById(R.id.button_AddCaseStep1Walkin);
		buttonFileBrowser = (Button) findViewById(R.id.button_AddCaseStep1Browser);
		buttonOk = (Button) findViewById(R.id.button_AddCaseStep1Ok);
		buttonCancle = (Button) findViewById(R.id.button_AddCaseStep1Cancle);

		//buttonChooseDoc.setOnClickListener(this);
		dialog = ProgressDialog.show(AddCaseStep1of4.this, "", "Uploading file...", true);
		addCaseListOfItems();
		
		buttonFileBrowser.setOnClickListener(this);
		buttonOk.setOnClickListener(this);
		buttonCancle.setOnClickListener(this);
		walkin.setOnClickListener(this);

	}

	
	 public boolean dispatchTouchEvent(MotionEvent ev) {	       
	        InputMethodManager imm = (InputMethodManager)getSystemService(Context.
                    INPUT_METHOD_SERVICE);
imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
	        return super.dispatchTouchEvent(ev);

	        } 
	 
	@Override
	public void onClick(View v) {
		if (v == buttonChooseDoc) {
			// Find AddCase List Of Items
			dialog = ProgressDialog.show(AddCaseStep1of4.this, "", "Uploading file...", true);
			addCaseListOfItems();

		}
		 else if (v == buttonCancle) {		
				
				Intent intent = new Intent(this, AddCaseQuestion1.class);
				startActivityForResult(intent, REQUEST_PICK_FILE);

				dialog.dismiss();
			}
		 else if (v == buttonFileBrowser) {
			System.out.println("workingthom");
			dialog = ProgressDialog.show(AddCaseStep1of4.this, "", "Uploading file...", true);
			Intent intent = new Intent(this, FilePicker.class);
			startActivityForResult(intent, REQUEST_PICK_FILE);

			dialog.dismiss();
		} else if (v == buttonOk) {
			
			String ed_text = edittextFile.getText().toString().trim();
			if(ed_text.isEmpty() || ed_text.length() == 0 || ed_text.equals("") || ed_text == null || itemCode.isEmpty() || itemCode.length() == 0 || itemCode.equals("") || itemCode == null)
			{
				Toast.makeText(AddCaseStep1of4.this, "Kindly Select Item / File", Toast.LENGTH_LONG).show();
			}
			else
			{
				dialog = ProgressDialog.show(AddCaseStep1of4.this, "", "Uploading file...", true);
				 new Thread(new Runnable() {
	                    public void run() {              
	                    	RESULTRES = uploadFile(fileName);
	                    	
	                    	
	                    }
	                }).start();  
				 
				/* if(RESULTRES==200)
   					 addCaseDocumentToRead();
   				 else
   					 Toast.makeText(AddCaseStep1of4.this, "File Not Uploading.. Check your File.", Toast.LENGTH_SHORT).show();*/
				 
				
				//Intent qIntent = new Intent(AddCaseStep1of4.this, AddCaseStep2of4.class);			
				//startActivity(qIntent);				
			}
			
			//convertBinary();
			//uploadfiletoserver();
			//dialog.dismiss();
			
		}
		else if(v==walkin)
		{
			Intent i = new Intent(AddCaseStep1of4.this, WalkInActivity.class);
			startActivity(i);
		}
		
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

                Log.i("uploadFile", "HTTP Response is : "+ serverResponseMessage + ": " + serverResponseCode);
                
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                StringBuilder sb = new StringBuilder();
                String line;
                while ((line = br.readLine()) != null) {
                    sb.append(line+"\n");
                }
                br.close(); JSONArray arry = new JSONArray(sb.toString());
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

                dialog.dismiss();  
                ex.printStackTrace();

                runOnUiThread(new Runnable() {
                    public void run() {

                        Toast.makeText(AddCaseStep1of4.this, "MalformedURLException", 
                                Toast.LENGTH_SHORT).show();
                    }
                });

                Log.e("Upload file to server", "error: " + ex.getMessage(), ex);  
            } catch (Exception e) {

                dialog.dismiss();  
                e.printStackTrace();

                runOnUiThread(new Runnable() {
                    public void run() {

                        Toast.makeText(AddCaseStep1of4.this, "Got Exception : see logcat ", 
                                Toast.LENGTH_SHORT).show();
                    }
                });
                Log.e("Upload file to server Exception", "Exception : "
                        + e.getMessage(), e);  
            }
            dialog.dismiss();       
            return serverResponseCode; 

        } // End else block 
    } 
	
	public void addCaseDocumentToRead() {
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
			System.out.println("Param Inputs");
			jsonObject.put("ItemCode", itemCode);
			System.out.println(itemCode);
			jsonObject.put("ItemName", itemName);
			System.out.println(itemName);
			jsonObject.put("FileName", FILEUPLOADRESULT);
			System.out.println(fileName);
			//jsonObject.put("sDoc", "");
			System.out.println(sb);
			jsonObject.put("CardCode", CardCodeResponse);
			System.out.println(CardCodeResponse);

			RequestParams params = new RequestParams();
			params.put("sJsonInput", jsonObject.toString());
			System.out.println("params");
			System.out.println(params);

			RestService.post(METHOD_ADDCASE_DOCUMENT, params, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);

				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					// TODO Auto-generated method stub
					System.out.println("Document Send  Confirmed");
					System.out.println(arg2);

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
					if (messageDisplay.isEmpty()) {
						//Toast.makeText(AddCaseStep1of4.this, "Somthing went wrong", Toast.LENGTH_SHORT).show();
						Intent iAddBack = new Intent(context, AddCaseStep2of4.class);
						iAddBack.putExtra("jsonArray", jsonResponse.toString());
						startActivity(iAddBack);
						dialog.dismiss();
					} else {
						Intent iAddBack = new Intent(context, AddCaseStep2of4.class);
						iAddBack.putExtra("jsonArray", jsonResponse.toString());
						startActivity(iAddBack);
						dialog.dismiss();

						Toast.makeText(AddCaseStep1of4.this, messageDisplay, Toast.LENGTH_SHORT).show();

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

	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data) {

		if (resultCode == RESULT_OK) {

			switch (requestCode) {

			case REQUEST_PICK_FILE:

				if (data.hasExtra(FilePicker.EXTRA_FILE_PATH)) {

					selectedFile = new File(data.getStringExtra(FilePicker.EXTRA_FILE_PATH));
					fileName = selectedFile.getPath();
					edittextFile.setText(fileName);
				}
				break;
			}
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
				//Toast.makeText(AddCaseStep1of4.this, "AddCase Item Found", Toast.LENGTH_SHORT).show();
				dialog.dismiss();
				// Simple Adapter for List
				SimpleAdapter simpleAdapter = new SimpleAdapter(AddCaseStep1of4.this, jsonItemList,
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
						//Toast.makeText(AddCaseStep1of4.this, "You Clicked at " + jsonItemList.get(position),
							//	Toast.LENGTH_SHORT).show();
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
}
