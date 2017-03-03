package com.example.entity;

import android.app.Notification;
import android.app.PendingIntent;
import android.widget.RemoteViews;

import com.example.service.Downloader;

public class DownloadUIInfo {
	
	private static int notificationIDStart = 10000;
	
	private int notificationID;
	private Downloader downloader;
	private RemoteViews remoteViews;
	private Notification notification;
	private PendingIntent pendingIntent;
	public DownloadUIInfo(Downloader downloader, 
			RemoteViews remoteViews, Notification notification){
		this.downloader = downloader;
		this.remoteViews = remoteViews;
		this.notification = notification;
		this.notificationID = ++notificationIDStart;
	}
	public void setDownloadUIInfo(Downloader downloader, 
			RemoteViews remoteViews, Notification notification, int notificationID){
		this.downloader = downloader;
		this.remoteViews = remoteViews;
		this.notification = notification;
		this.notificationID = ++notificationIDStart;
	}
	
	public int getNotificationID(){
		return notificationID;
	}
	public Downloader getDownloader(){
		return downloader;
	}
	public RemoteViews getRemoteViews(){
		return remoteViews;
	}
	public Notification getNotification(){
		return notification;
	}
	public void setNotificationID(int notificationID){
		this.notificationID = notificationID;
	}
	public void setDownloader(Downloader downloader){
		this.downloader = downloader;
	}
	public void setRemoteViews(RemoteViews remoteViews){
		this.remoteViews = remoteViews;
	}
	public void setNotification(Notification notification){
		this.notification = notification;
	}
	public PendingIntent getPendingIntent(){
		return pendingIntent;
	}
	public void setPendingIntent(PendingIntent pendingIntent){
		this.pendingIntent = pendingIntent;
	}
    public void clear(){
    	downloader = null;
    	remoteViews = null;
    	notification = null;
    	pendingIntent = null;
    }
}
