package com.abeo.tia.noordin;

import java.util.ArrayList;

import com.navigationdrawer.NavDrawerItem;
import com.navigationdrawer.NavDrawerListAdapter;

import abeo.tia.noordin.R;
import android.annotation.SuppressLint;
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
	String tb_tm_new_value = null, tb_tm_closed_value = null, tb_lm_new_value = null, tb_lm_closed_value = null, FirstName = null;
	Button walkin;
	public String tag = "";
	double tb_tm_new_value_num = 0, tb_tm_close_value_num = 0, tb_lm_new_value_num = 0, tb_lm_close_value_num = 0;

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
				Toast.makeText(DashBoardActivity.this, "Walk-in button click", Toast.LENGTH_LONG).show();
				Log.e(tag, "Walk-in Button Clicked");
				Intent i = new Intent(DashBoardActivity.this, WalkInActivity.class);
				startActivity(i);
			}
		});

		// Find JSON DATA in Bundle
		Intent bundle = getIntent();

		// Find Progress bar value for JSONResponse
		tb_tm_new_value = bundle.getStringExtra("TB_TM_NewCasesPro");
		System.out.println(tb_tm_new_value);

		tb_tm_closed_value = bundle.getStringExtra("TB_TM_ClosedCasesPro");
		System.out.println(tb_tm_closed_value);

		tb_lm_new_value = bundle.getStringExtra("TB_LM_NewCasesPro");
		System.out.println(tb_lm_new_value);

		tb_lm_closed_value = bundle.getStringExtra("TB_LM_ClosedCasesPro");
		System.out.println(tb_lm_closed_value);
		
		FirstName = bundle.getStringExtra("FirstName");
		System.out.println(FirstName);

		SharedPreferences prefLogin = getSharedPreferences("FirstName", Context.MODE_PRIVATE);

		// We need an editor object to make changes
		SharedPreferences.Editor edit = prefLogin.edit();
		// Set/Store data
		edit.putString("FirstName", FirstName);

		// Commit the changes
		edit.commit();
		
		// Find the SharedPreferences value
					SharedPreferences FirstName = getSharedPreferences("FirstName", Context.MODE_PRIVATE);
					
					String FirName = FirstName.getString("FirstName", "");
					TextView welcome = (TextView)findViewById(R.id.textView_welcome);
					
					welcome.setText("Welcome "+FirName);
		
		// Find Progress Bar Tb_Tm_NewCaess
		ProgBar_Tb_Tm_NewCaess = (ProgressBar) findViewById(R.id.progressBar_tb_tm_new_case);

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

		// Find Button response for priority, action & open
		Button_PRIORITY = (Button) findViewById(R.id.button_prority);
		priority_response = bundle.getStringExtra("PRIORITY");
		System.out.println(priority_response);
		Button_PRIORITY.setText(priority_response);

		Button_ACTION = (Button) findViewById(R.id.button_action);
		action_response = bundle.getStringExtra("ACTION");
		System.out.println(action_response);
		Button_ACTION.setText(action_response);

		Button_OPEN = (Button) findViewById(R.id.button_open);
		open_response = bundle.getStringExtra("OPEN");
		System.out.println(open_response);
		Button_OPEN.setText(open_response);
	}

}