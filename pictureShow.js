var is_login=true;
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
          is_good=true;
    }
    else{
    	 $num.attr("value",parseInt(number) - 1);
    	 is_good=false;
    }
});
$("#collect").click(function() {
    var $num = $(this);
    var number = $num.attr("value");
    if(is_collect==false){ 
       is_collect=true;
       $num.attr("value",parseInt(number)+1);
   }
    else{
    	is_collect=false;
    	$num.attr("value",parseInt(number)-1);

   }
    

});
})

$("#comment").click(function(){
	$("#submit").show();
})