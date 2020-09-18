// JavaScript Document$(function(){
localStorage.setItem("picId","1");
localStorage.setItem("userId","2");


//var source=localStorage.getItem("scr");
var picId=localStorage.getItem("picId");
var userId=localStorage.getItem("userId");
var settings = {
  "url": "http://172.81.239.44/Account/getUserInfo?userId=2",
  "method": "GET",
  "timeout": 0,
  "headers":{"Authorization":"Bearer "+"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4NTRjZGZmOS03ODQ2LTRjYzAtOWNjMy01ZjhkOTJjMmY5NjAiLCJuYW1lIjoicm9vdCIsIm5iZiI6MTYwMDQxOTI1MiwiZXhwIjoxNjAwNDE5ODUyLCJpc3MiOiJqd3RJc3N1ZXJ0ZXN0IiwiYXVkIjoiand0QXVkaWVuY2V0ZXN0In0.e7tPDpt2RFxEsqL_cLsp8K32JJbQSAY-RGAeuw95R9w"},
};

$.ajax(settings).done(function (response) {
  console.log(response);
	console.log(response.userName);
	$("#user").append("<h3>"+response.userName+"</h3>");
});

var settings = {
  "url": "http://172.81.239.44/Account/getProfileInfo?userId=2",
  "method": "GET",
  "timeout": 0,
  "headers":{"Authorization":"Bearer "+"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4NTRjZGZmOS03ODQ2LTRjYzAtOWNjMy01ZjhkOTJjMmY5NjAiLCJuYW1lIjoicm9vdCIsIm5iZiI6MTYwMDQxOTI1MiwiZXhwIjoxNjAwNDE5ODUyLCJpc3MiOiJqd3RJc3N1ZXJ0ZXN0IiwiYXVkIjoiand0QXVkaWVuY2V0ZXN0In0.e7tPDpt2RFxEsqL_cLsp8K32JJbQSAY-RGAeuw95R9w"},
};

$.ajax(settings).done(function (response) {
  console.log(response);
	console.log(response.starNum);
	$("#icon1").append("<span>"+response.starNum+"</span>");
	$("#icon2").append("<span>"+response.followNum+"</span>");
	$("#icon3").append("<span>"+response.likeNum+"</span>");
	$("#icon4").append("<span>"+response.commentNum+"</span>");
});

/*
	var settings = {
  "url": "http://172.81.239.44/Account/getProfilePicture?userId=1",
  "method": "GET",
  "timeout": 0,
  "headers":{"Authorization":"Bearer "+"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4NTRjZGZmOS03ODQ2LTRjYzAtOWNjMy01ZjhkOTJjMmY5NjAiLCJuYW1lIjoicm9vdCIsIm5iZiI6MTYwMDQxOTI1MiwiZXhwIjoxNjAwNDE5ODUyLCJpc3MiOiJqd3RJc3N1ZXJ0ZXN0IiwiYXVkIjoiand0QXVkaWVuY2V0ZXN0In0.e7tPDpt2RFxEsqL_cLsp8K32JJbQSAY-RGAeuw95R9w"},
};
$.ajax(settings).done(function(response) {
  console.log(response);
	var arr=response.upload;
	var container=document.querySelector("#page1");
	arr.forEach(function(item,index){
		var div=document.createElement('div');
		div.setAttribute("class","thumbnail");
		var img=document.createElement('img');
		var pictitle=document.createElement('p');
		pictitle.innerHTML="<h4>"+item.thatpicture.p_info+"</h4>";
		pictitle.setAttribute("margin-top","10px");
		pictitle.setAttribute("tag","h4");
		//pictitle.setAttribute("size","6");
		//pictitle.setAttribute("color","#52BAD5");
		console.log(item.thatpicture.p_info);
		img.src=item.thatpicture.p_url;
		img.setAttribute("class","cards");
		img.setAttribute("id","img1");
		div.appendChild(img);
		div.appendChild(pictitle);
		container.appendChild(div);
})
});
*/
	var settings = {
  "url": "http://172.81.239.44/Account/getProfilePicture?userId=1",
  "method": "GET",
  "timeout": 0,
  "headers":{"Authorization":"Bearer "+"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4NTRjZGZmOS03ODQ2LTRjYzAtOWNjMy01ZjhkOTJjMmY5NjAiLCJuYW1lIjoicm9vdCIsIm5iZiI6MTYwMDQxOTI1MiwiZXhwIjoxNjAwNDE5ODUyLCJpc3MiOiJqd3RJc3N1ZXJ0ZXN0IiwiYXVkIjoiand0QXVkaWVuY2V0ZXN0In0.e7tPDpt2RFxEsqL_cLsp8K32JJbQSAY-RGAeuw95R9w"},
};
$.ajax(settings).done(function(response) {
  console.log(response);
	var arr=response.upload;
	var container=document.querySelector("#page2");
	arr.forEach(function(item,index){
			var div=document.createElement('div');
			div.setAttribute("class","thumbnail");
			var img=document.createElement('img');
			img.src=item.thatpicture.p_url;
			img.setAttribute("class","cards");
			div.appendChild(img);
			container.appendChild(div);
})
});

