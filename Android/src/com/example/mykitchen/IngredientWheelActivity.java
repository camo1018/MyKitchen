package com.example.mykitchen;

import android.os.Bundle;
import android.view.View;
import android.app.Activity;
import android.view.Menu;
import java.util.ArrayList;
import android.widget.TextView;
public class IngredientWheelActivity extends Activity {
	int count = 0;
	ArrayList<String> ingredients;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_ingredient_wheel);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.ingredient_wheel, menu);
		return true;
	}
	
	public void onClick(View view){
		TextView textview = (TextView)findViewById(R.id.textViewIng);
		if(view.getId()==R.id.buttonP){
			if(count==0)count = ingredients.size()-1;
			else count--;
			textview.setText(ingredients.get(count));
		}
		if(view.getId()==R.id.buttonN){
			if(count==ingredients.size()-1) count=0;
			else count++;
			textview.setText(ingredients.get(count));
		}
		if(view.getId()==R.id.buttonS){
			
		}
	}

}
