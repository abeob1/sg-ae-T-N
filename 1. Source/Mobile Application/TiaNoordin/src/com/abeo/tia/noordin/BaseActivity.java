package com.abeo.tia.noordin;

import java.util.ArrayList;
import abeo.tia.noordin.R;
import com.navigationdrawer.NavDrawerItem;
import com.navigationdrawer.NavDrawerListAdapter;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.Configuration;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.support.v4.app.ActionBarDrawerToggle;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.AlertDialog;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.RelativeLayout;

@SuppressWarnings("deprecation")
@SuppressLint("NewApi")
public class BaseActivity extends ActionBarActivity {
	final Context context = this;
	private DrawerLayout mDrawerLayout;
	private ListView mDrawerList;
	private ActionBarDrawerToggle mDrawerToggle;
	protected RelativeLayout _completeLayout, _activityLayout;
	// nav drawer title
	private CharSequence mDrawerTitle;

	// used to store app title
	private CharSequence mTitle;

	private ArrayList<NavDrawerItem> navDrawerItems;
	private NavDrawerListAdapter adapter;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.drawer);
		// if (savedInstanceState == null) {
		// // on first time display view for first nav item
		// // displayView(0);
		// }
	}

	public void set(String[] navMenuTitles, TypedArray navMenuIcons) {
		mTitle = mDrawerTitle = getTitle();

		mDrawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
		mDrawerList = (ListView) findViewById(R.id.left_drawer);

		navDrawerItems = new ArrayList<NavDrawerItem>();

		// adding nav drawer items
		if (navMenuIcons == null) {
			for (int i = 0; i < navMenuTitles.length; i++) {
				navDrawerItems.add(new NavDrawerItem(navMenuTitles[i]));
			}
		} else {
			for (int i = 0; i < navMenuTitles.length; i++) {
				navDrawerItems.add(new NavDrawerItem(navMenuTitles[i], navMenuIcons.getResourceId(i, -1)));
			}
		}

		mDrawerList.setOnItemClickListener(new SlideMenuClickListener());

		// setting the nav drawer list adapter
		adapter = new NavDrawerListAdapter(getApplicationContext(), navDrawerItems);
		mDrawerList.setAdapter(adapter);

		// enabling action bar app icon and behaving it as toggle button
		getSupportActionBar().setDisplayHomeAsUpEnabled(true);
		getSupportActionBar().setHomeButtonEnabled(true);
		// getSupportActionBar().setIcon(R.drawable.ic_drawer);
		getSupportActionBar().setHomeAsUpIndicator(R.drawable.ic_drawer);

		mDrawerToggle = new ActionBarDrawerToggle(this, mDrawerLayout, R.drawable.ic_drawer, // nav
																								// menu
																								// toggle
																								// icon
				R.string.app_name, // nav drawer open - description for
				// accessibility
				R.string.app_name // nav drawer close - description for
		// accessibility
		) {
			public void onDrawerClosed(View view) {
				getSupportActionBar().setTitle(mTitle);
				// calling onPrepareOptionsMenu() to show action bar icons

				supportInvalidateOptionsMenu();
			}

			public void onDrawerOpened(View drawerView) {
				getSupportActionBar().setTitle(mDrawerTitle);
				// calling onPrepareOptionsMenu() to hide action bar icons
				supportInvalidateOptionsMenu();
			}
		};
		mDrawerLayout.setDrawerListener(mDrawerToggle);

	}

	private class SlideMenuClickListener implements ListView.OnItemClickListener {
		@Override
		public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
			// display view for selected nav drawer item
			displayView(position);
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// getSupportMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		if (item.getItemId() == android.R.id.home) {
			if (mDrawerLayout.isDrawerOpen(mDrawerList)) {
				mDrawerLayout.closeDrawer(mDrawerList);
			} else {
				mDrawerLayout.openDrawer(mDrawerList);
			}
		}

		return super.onOptionsItemSelected(item);
	}

	/***
	 * Called when invalidateOptionsMenu() is triggered
	 */
	@Override
	public boolean onPrepareOptionsMenu(Menu menu) {
		// if nav drawer is opened, hide the action items
		// boolean drawerOpen = mDrawerLayout.isDrawerOpen(mDrawerList);
		// menu.findItem(R.id.action_settings).setVisible(!drawerOpen);
		return super.onPrepareOptionsMenu(menu);
	}

	/**
	 * Diplaying fragment view for selected nav drawer list item
	 */
	private void displayView(int position) {

		switch (position) {
		case 0:
			Intent intent = new Intent(BaseActivity.this, FirstActivity.class);
			startActivity(intent);
			// finish();// finishes the current activity
			break;
		case 1:
			Intent intentProperty = new Intent(context, PropertyActivity.class);
			startActivity(intentProperty);
			// finish();// finishes the current activity
			break;
		case 2:
			Intent intent2 = new Intent(context, IndividualActivity.class);
			startActivity(intent2);
			// finish();
			break;
		case 3:
			Intent intent3 = new Intent(context, CorporateActivity.class);
			startActivity(intent3);
			// finish();
			break;
		case 4:
			Intent intent4 = new Intent(BaseActivity.this, AddCaseQuestion1.class);
			startActivity(intent4);
			// finish();
			break;

		case 5:
			Intent intent5 = new Intent(BaseActivity.this, DashBoardActivity.class);
			startActivity(intent5);
			// finish();
			break;
		case 6:

			AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(context);

			// set title
			alertDialogBuilder.setTitle("Logout");

			// set dialog message
			alertDialogBuilder.setMessage("Click yes to exit!").setCancelable(false)
					.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int id) {
							// if this button is clicked, close
							// current activity
							// MainActivity.this.finish();
							Intent intentLogout = new Intent(context, LoginActivity.class);
							intentLogout.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
							startActivity(intentLogout);
							finish();

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
			break;
		default:
			break;
		}

		// update selected item and title, then close the drawer
		mDrawerList.setItemChecked(position, true);
		mDrawerList.setSelection(position);
		mDrawerLayout.closeDrawer(mDrawerList);
	}

	@Override
	public void setTitle(CharSequence title) {
		mTitle = title;
		getActionBar().setTitle(mTitle);
	}

	/**
	 * When using the ActionBarDrawerToggle, you must call it during
	 * onPostCreate() and onConfigurationChanged()...
	 */

	@Override
	protected void onPostCreate(Bundle savedInstanceState) {
		super.onPostCreate(savedInstanceState);
		// Sync the toggle state after onRestoreInstanceState has occurred.
		mDrawerToggle.syncState();
	}

	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		super.onConfigurationChanged(newConfig);
		// Pass any configuration change to the drawer toggls
		mDrawerToggle.onConfigurationChanged(newConfig);
	}
}
