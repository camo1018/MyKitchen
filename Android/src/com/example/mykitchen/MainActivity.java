package com.example.mykitchen;

import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;
import android.view.View;
import android.content.Intent;

public class MainActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
	
	public void goTo(View view){
		if(view.getId() == R.id.buttonSearch){
			Intent intent = new Intent(this, SearchRecipeActivity.class);
			startActivity(intent);
		}
		if(view.getId() == R.id.buttonKitchen){
			Intent intent = new Intent(this, MyKitchenActivity.class);
			startActivity(intent);
		}
		if(view.getId() == R.id.buttonFavorites){
			Intent intent = new Intent(this, MyFavoritesActivity.class);
			startActivity(intent);
		}
		if(view.getId() == R.id.buttonProfile){
			Intent intent = new Intent(this, MyProfileActivity.class);
			startActivity(intent);
		}
	}

}
