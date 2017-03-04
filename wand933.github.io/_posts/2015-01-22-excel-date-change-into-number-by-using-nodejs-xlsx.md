---
layout: post
title:  "excel date change into number by using nodejs xlsx"
date:   2015-01-22
categories: nodejs
author: wangd933
---

需求：将excel数据导入mysql，通过web做，不依赖本地app。  
工具：nodejs，xlsx  
问题：由于xlsx本身的转json函数对于excel表的格式有要求（每一列必须有一个键），  
实际无法满足这一情况，所以手动将excel转换为json格式，但是却遇到excel里  
的date类型数据转换后变为数字  
解决：查看sheet_to_json函数，发现其使用了format_cell,so question is solved.

