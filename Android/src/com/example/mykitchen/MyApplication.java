package com.example.mykitchen;

import java.util.ArrayList;

import android.app.Application;

public class MyApplication extends Application {
	
	private int userId;
	
	public int getUserId() {
		return userId;
	}
	
	public void setUserId(int id) {
		userId = id;
	}
	
	private ArrayList<Integer> recipes;
	
	public ArrayList<Integer> getRecipes() {
		return recipes;
	}

	public void setRecipes(ArrayList<Integer> recipes) {
		this.recipes = recipes;
	}

	
	
	
}