"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:4398/chatHub")
    .withAutomaticReconnect() //断线自动重连
    .build();

connection.start();

//自动重连成功后的处理
connection.onreconnected(connectionId => {
    alert(connectionId);
});


//---消息---
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("SendMessageResponse", function (res) {
    if (res && res.status == 0) {
        var li = document.createElement("li");
        li.textContent = res.message;
        document.getElementById("messagesList").appendChild(li);
    } else {
        alert(res.message);
    }
});
//---消息---


//---登录---
document.getElementById("btnLogin").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("Login", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("LoginResponse", function (res) {
    if (res && res.status == 0) {
        sessionStorage.setItem('curuser', res.data);
        alert(res.message);
        getUsers();
    }
    else {
        alert('登录失败！');
    }
});
//---登录---


//获取在线用户
function getUsers() {
    connection.invoke("GetUsers").catch(function (err) {
        return console.error(err.toString());
    });
    connection.on("GetUsersResponse", function (res) {
        if (res && res.status == 0) {
            var _lis = '<li>在线用户：</li>';
            for (var i = 0; i < res.onlineUser.length; i++) {
                _lis += `<li>${res.onlineUser[i].userName}</li>`;
            }
            document.getElementById("usersList").innerHTML = _lis;
        }
    });
}
