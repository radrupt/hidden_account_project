LOCAL_PATH := $(call my-dir)  
include $(CLEAR_VARS)  
OPENCV_LIB_TYPE:=STATIC
include ../OpenCV-SDK/native/jni/OpenCV.mk  
LOCAL_SRC_FILES  := ImageProc.cpp  
LOCAL_MODULE     := image_proc  
include $(BUILD_SHARED_LIBRARY)