package com.example.entity;

public class DownloadInfo {
	private int threadId;//������id  
    private int startPos;//��ʼ��  
    private int endPos;//������  
    private int compeleteSize;//��ɶ�  
    private String url;//�����������ʶ  
    public DownloadInfo(int threadId, int startPos, int endPos,  
            int compeleteSize,String url) {  
        this.threadId = threadId;  
        this.startPos = startPos;  
        this.endPos = endPos;  
        this.compeleteSize = compeleteSize;  
        this.url=url;  
    }  
    public DownloadInfo() {  
    }  
    public String getUrl() {  
        return url;  
    }  
    public void setUrl(String url) {  
        this.url = url;  
    }  
    public int getThreadId() {  
        return threadId;  
    }  
    public void setThreadId(int threadId) {  
        this.threadId = threadId;  
    }  
    public int getStartPos() {  
        return startPos;  
    }  
    public void setStartPos(int startPos) {  
        this.startPos = startPos;  
    }  
    public int getEndPos() {  
        return endPos;  
    }  
    public void setEndPos(int endPos) {  
        this.endPos = endPos;  
    }  
    public int getCompeleteSize() {  
        return compeleteSize;  
    }  
    public void setCompeleteSize(int compeleteSize) {  
        this.compeleteSize = compeleteSize;  
    }  
  
    @Override  
    public String toString() {  
        return "DownloadInfo [threadId=" + threadId  
                + ", startPos=" + startPos + ", endPos=" + endPos  
                + ", compeleteSize=" + compeleteSize +"]";  
    }  
}
