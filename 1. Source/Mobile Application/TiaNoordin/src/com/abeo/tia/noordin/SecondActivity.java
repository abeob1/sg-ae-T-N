package com.abeo.tia.noordin;

import android.annotation.SuppressLint;
import android.content.res.TypedArray;
import android.os.Bundle;
import abeo.tia.noordin.R;

@SuppressLint("Recycle")
public class SecondActivity extends BaseActivity{
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_second);
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items); // load
		// titles
		// from
		// strings.xml

		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);// load
																					// icons
																					// from
		// strings.xml

		set(navMenuTitles, navMenuIcons);
	}
}
