var is_login=true;
var is_follow=false;
var image=$(".picture");
var imageSize=new Image();
imageSize.src=image.attr("src");
var imageHeight=imageSize.height;
var imageWidth=imageSize.width;
var rate2=imageWidth/imageHeight;
var rate1=1000/600;

$(document).ready(function(){
  if(is_login==true){
  	$("#login").css("display", "none");
	$("#register").css("display", "none");
	$(".headimg").css("display","inline")
  }
  else{
  	$("#login").css("display", "inline");
	$("#register").css("display", "inline");
	$(".headimg").css("display","none")
  }
  if(rate1>=rate2){
  	$(".picture").css("width",600*rate2);
  }
  else{
  	$(".picture").css("height",1000/rate2);
  }
  

});
var is_good=false;
var is_collect=false;

$(function() {
$("#good").click(function() {
    var $num = $(this);
    var number = $num.attr("value");
    if(is_good==false){
          $num.attr("value",parseInt(number) + 1);
          $(".good_label").css("color","#f957be");
          is_good=true;
    }
    else{
    	 $num.attr("value",parseInt(number) - 1);
        $(".good_label").css("color","#a8a2a2");
    	 is_good=false;
    }
});
$("#collect").click(function() {
    var $num = $(this);
    var number = $num.attr("value");
    if(is_collect==false){ 
       is_collect=true;
       $num.attr("value",parseInt(number)+1);
       $(".collect_label").css("color","#f957be");
   }
    else{
    	is_collect=false;
    	$num.attr("value",parseInt(number)-1);
      $(".collect_label").css("color","#a8a2a2");
   }
    

});
})

$("#comment").click(function(){
	$("#submit").show();
})

$(".publisher_info1 button").click(function(){
    if(is_follow==false){ 
        $(this).css("background-image","url(./img/follow1.png)");
        is_follow=true;

    }
    else{
       $(this).css("background-image","url(./img/follow.png)");
        is_follow=false;

    }

})

$("#download").click(function(){
    $(".pop_up").css("display","block");
    $(".publisher_info").css("opacity","0.5");
    $(".title-style").css("opacity","0.5");
    $(".pictureShow").css("opacity","0.5");
    $(".picture_info").css("opacity","0.5");
    $(".comment").css("opacity","0.5");
    $(".other_comment").css("opacity","0.5");
    var value=$("#download").attr("value");
    $(".txt").html("您是否确定用"+value+"个硬币购买本图片？");
})

$("#confirm").click(function(){
    $(".pop_up").css("display","none");
    $(".publisher_info").css("opacity","1.0");
    $(".title-style").css("opacity","1.0");
    $(".pictureShow").css("opacity","1.0");
    $(".picture_info").css("opacity","1.0");
    $(".comment").css("opacity","1.0");
    $(".other_comment").css("opacity","1.0");
    alert("交易成功！开始下载...");

})

$("#cancel").click(function(){
    $(".pop_up").css("display","none");
    $(".publisher_info").css("opacity","1.0");
    $(".title-style").css("opacity","1.0");
    $(".pictureShow").css("opacity","1.0");
    $(".picture_info").css("opacity","1.0");
    $(".comment").css("opacity","1.0");
    $(".other_comment").css("opacity","1.0");

})
