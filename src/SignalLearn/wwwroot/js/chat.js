//创建一个Signal连接器，配置连接地址
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

//配置客户端，监听服务器 "ReceiveMessage" 事件，并接受服务器返回的消息内容:用户名和消息
connection.on("ReceiveMessage", (user, message) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");  //消息处理
    const encodedMsg = user + " says " + msg;                //处理消息
    const li = document.createElement("li");
    li.textContent = encodedMsg;                             //创建页面li元素
    document.getElementById("messagesList").appendChild(li);  //附加到DOM中
});

//启动客户端配置
connection.start().catch(err => console.error(err.toString()));

//消息发送事件
document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    //向服务器发送 "SendMessage" 事件，并包含消息用户名，消息内容
    connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
    event.preventDefault();
});

//--------------------------- 向服务器发送消息，并回接受服务器的应答-----------------------
///测试调用SendMessageToCaller
document.getElementById("test1").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    //向服务器发送 "SendMessageToCaller" 事件，并包含消息用户名，消息内容
    connection.invoke("SendMessageToCaller", message).catch(err => console.error(err.toString()));
    event.preventDefault();
});

//配置客户端，监听服务器 "SendMessageToCaller" 事件，并接受服务器应答，只有自己会收到消息
connection.on("SendMessageToCaller", (message) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");  //消息处理
    const encodedMsg = msg;                //处理消息
    const li = document.createElement("li");
    li.textContent = encodedMsg;                             //创建页面li元素
    document.getElementById("messagesList").appendChild(li);  //附加到DOM中
});
//--------------------------- 向服务器发送消息，并回接受服务器的应答-----------------------



//--------------------------- 分组消息测试 -----------------------
///加入分组
document.getElementById("test2").addEventListener("click", event => {
    const message = document.getElementById("messageInput").value;
    //向服务器发送 "SendMessageToGroups" 事件，并包含消息用户名，消息内容
    connection.invoke("SendMessageToGroups", message).catch(err => console.error(err.toString()));
    event.preventDefault();
});

//配置客户端，监听服务器 "SendMessageToGroups" 事件，并接受服务器应答
connection.on("SendMessageToGroups", (message) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");  //消息处理
    const encodedMsg = msg;                //处理消息
    const li = document.createElement("li");
    li.textContent = encodedMsg;                             //创建页面li元素
    document.getElementById("groupMessageList").appendChild(li);  //附加到DOM中
});
//--------------------------- 向服务器发送消息，并回接受服务器的应答-----------------------










