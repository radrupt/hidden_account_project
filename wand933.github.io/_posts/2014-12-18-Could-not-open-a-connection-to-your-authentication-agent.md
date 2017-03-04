---
layout: post
title:  "Could not open a connection to your authentication agent"
date:   2014-12-18
categories: git
author: wangd933
---

`way 1:`

>1. ssh-agent
2. eval 'ssh-agent -s'
3. ssh-add 'your id_rsa'

`way 2:`

>1. ssh-agent
2. eval $(ssh-agent)
3. ssh-add 'your id_rsa`

`reference:`

>1.[could-not-open-a-connection-to-your-authentication-agent]<br>2.[fix-could-not-open-a-connection-to-your-authentication-agent-when-using-ssh-add]

[could-not-open-a-connection-to-your-authentication-agent]: http://stackoverflow.com/questions/17846529/could-not-open-a-connection-to-your-authentication-agent
[fix-could-not-open-a-connection-to-your-authentication-agent-when-using-ssh-add]: http://stackoverflow.com/questions/17846529/could-not-open-a-connection-to-your-authentication-agent

