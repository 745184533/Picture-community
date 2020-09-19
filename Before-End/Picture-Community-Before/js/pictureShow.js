var is_login=false;//是否处于登录状态
var is_follow=false;//当前用户是否关注发布者
var is_good=false;//当前用户是否点赞当前图片
var is_collect=false;//当前用户是否收藏当前图片
var is_download=false;//当前用户是否已经购买并下载当前图片
var pic_price=0;//当前图片价格
var publisher_id;//当前图片发布者id
var publisher_name;//当前图片发布者名字
var comment_num;//当前图片的评论数
var pic_id;//图片id
var user_id;//用户id
var user_name;//用户昵称
var downloads;//当前图片下载量
var likes;//当前图片点赞数
var collects;//当前图片收藏数
var coins;//当前用户硬币数
var source;//当前图片url
var allComments;//当前图片的评论
var token;


token=localStorage.Token;//获取token

//localStorage.setItem('picId','2');
//localStorage.setItem('userId','5');
user_id=localStorage.userId;//获取用户昵称
if(user_id!=null){//判断用户是否登录
  is_login=true;
}
else{
  is_login=false;
  user_id="1";
}


user_name=localStorage.userName;//取当前用户昵称
pic_id=localStorage.getItem("picId");//取图片id
console.log("islogin?"+is_login);
console.log("userId:"+user_id);
console.log("picId:"+pic_id);

$(document).ready(function(){
  
 var settings = {
  "url": "http://172.81.239.44/Picture/getPicViewInfo?userID="+user_id+"&"+"picID="+pic_id,
  "method": "GET",
  "timeout": 0,

  };
  
$.ajax(settings).done(function (response) {
   console.log(response);
   source=response.picUrl;//取图片的url
   downloads=response.downloads;//取图片下载量
   likes=response.numberLike;//取图片点赞数
   collects=response.numberFavorite;//取图片收藏数
   
   var imageWidth = response.picWidth;//图片宽                    
   var imageHeight = response.picHeight;//图片高
   var rate2=imageWidth/imageHeight;
   var rate1=1000/600;
   $(".picture").attr("src",source); //替换图片 
   $(".pixel").html(imageWidth+"*"+imageHeight);//图片尺寸
   $(".brief").html(response.picInfo);//图片简介
   $(".time").html(response.uploadtime);//图片上传时间
   $(".downloads").html(downloads);//图片下载量
   $("#good").val(likes);//图片点赞数
   $("#collect").val(collects);//图片收藏数
   var tags=response.picTags;//图片标签
   if(tags.length!=0){
      tags.forEach( function(item){//显示图片标签
        if(item.tag_name!="1"&&item.tag_name!="2"){
          $(".tag").append(item.tag_name+" ");
        }
        
      });
    }
   else{
       $(".tag").html("none");
    }
     
   if(imageHeight>600||imageWidth>1000){//设置图片显示尺寸
     if(rate1>=rate2){
       $(".picture").css("width",600*rate2);
     }
     else{
       $(".picture").css("height",1000/rate2);
       var padding=(600-1000/rate2)/2;
       $(".pictureShow").css("padding-top",padding);
     }
   }
   else{
     var padding=(600-imageHeight)/2;
     $(".pictureShow").css("padding-top",padding);
     $(".picture").css("width",imageWidth);
     $(".picture").css("height",imageHeight);
   }

   is_good=response.picLike;//当前用户是否点赞
   is_collect=response.picStar;//当前用户是否收藏
   is_follow=response.publisherFollow;//当前用户是否关注发布者
   is_download=response.hasDownload;//当前用户是否已经购买并下载图片
   pic_price=response.pirce;//当前图片价格
   publisher_id=response.publisherId;//当前图片发布者id
   publisher_name=response.uploadName;//当前图片发布者昵称
   console.log("publisher_id:"+publisher_id);
   console.log("download?"+is_download);
   console.log("is_follow?"+is_follow);
   $(".publisher_info1 label").html(publisher_name);//发布者昵称
   
   if(is_login==true){//设置导航栏
     
     $(".comments").css("display","block");
     $(".restrict").css("display","none");
    
     $(".comment_publisher label").html("@"+user_name);
     if(is_good==false){//设置点赞状态
        $(".good_label").css("color","#a8a2a2");
      }
     else{
        $(".good_label").css("color","#f957be");
      }
      if(is_collect==false){//设置收藏状态 
        $(".collect_label").css("color","#a8a2a2");
      }
      else{
        $(".collect_label").css("color","#f957be");
      }
      if(is_follow==false){//设置关注状态 
        $(".publisher_info1 button").css("background-image","url(./img/follow.png)");
      }
      else{
        $(".publisher_info1 button").css("background-image","url(./img/follow1.png)");
      }
    }
    else{//未登录时
      
      $(".comments").css("display","none");
      $(".restrict").css("display","block");
      $(".good_label").css("color","#a8a2a2");
      $(".collect_label").css("color","#a8a2a2");
      $(this).css("background-image","url(./img/follow.png)");
    }

    $("#download").attr("value",pic_price);//图片价格
           
});

/*if(is_login==true){
  var settings = {
    "url": "http://172.81.239.44/Account/getUserInfo?userId="+user_id,
    "method": "GET",
    "timeout": 0,
  };

  $.ajax(settings).done(function (response) {
    console.log(response);
    user_name=response.userName;
    $(".headimg label").html("Hello,"+user_name+"!");
    $(".comment_publisher label").html("@"+user_name);
  });
}*/
 
 
 showComments();//展示图片评论

 $.ajax({//获取用户钱币个数
      "url": "http://172.81.239.44/Wallet/getWalletInfo?userId="+user_id,
      "method": "GET",
      "timeout": 0,
      "headers": {
        "Authorization":"Bearer "+token
      },
      success:function(response){
        console.log(response);
        if(response.success==true){
          coins=response.nowCoin;
          console.log("coins:"+coins);
        }
        
      },
      error:function(response){
         console.log("getwalletErr!");
      }
});

 
  
});


$(function() {
$("#good").click(function() {//点赞和取消点赞
    var $num = $(this);
    var number = $num.attr("value");
    if(is_login==true){
      if(is_good==false){
        
          var settings = {
            "url": "http://172.81.239.44/Picture/likePicture?userId="+user_id+"&picId="+pic_id+"&type=LK",
            "method": "POST",
            "timeout": 0,
          };
          $.ajax(settings).done(function (response) {
             console.log(response);
             if(response.success==true){
               $num.attr("value",parseInt(number) + 1);
               $(".good_label").css("color","#f957be");
               is_good=true;
             }
             else{
               alert("失败！");
             }
          });
      }
      else{
    	 
        var settings = {
           "url": "http://172.81.239.44/Picture/likePicture?userId="+user_id+"&picId="+pic_id+"&type=LK",
           "method": "POST",
           "timeout": 0,
        };
        $.ajax(settings).done(function (response) {
           console.log(response);
           if(response.success==true){
             $num.attr("value",parseInt(number) - 1);
             $(".good_label").css("color","#a8a2a2");
             is_good=false;
           }
           else{
            alert("失败！");
           }
        });
      }
   }
   else{
    alert("请先登录！！！");
    window.location.href="loginPage.html";
    //window.open("loginPage.html");
   }

});
$("#collect").click(function() {//收藏和取消收藏
    var $num = $(this);
    var number = $num.attr("value");
    if(is_login==true){
      if(is_collect==false){ 
        
        var settings = {
           "url": "http://172.81.239.44/Picture/favoritePicture?userId="+user_id+"&picId="+pic_id,
           "method": "POST",
           "timeout": 0,
        };
        $.ajax(settings).done(function (response) {
           console.log(response);
           if(response.success==true){
            is_collect=true;
            $num.attr("value",parseInt(number)+1);
            $(".collect_label").css("color","#f957be");
           }
           else{
            alert("失败！");
           }
        });
       
     }
      else{
    	  
        var settings = {
           "url": "http://172.81.239.44/Picture/favoritePicture?userId="+user_id+"&picId="+pic_id,
           "method": "POST",
           "timeout": 0,
        };
        $.ajax(settings).done(function (response) {
           console.log(response);
           if(response.success==true){
            is_collect=false;
            $num.attr("value",parseInt(number)-1);
            $(".collect_label").css("color","#a8a2a2");
           }
           else{
            alert("失败！");
           }
        });
      
     }
   }
   else{
    alert("请先登录！！！");
    window.location.href="loginPage.html";
    //window.open("loginPage.html");
   }
    

});
})


$(".publisher_info1 button").click(function(){//关注和取消关注
  if(is_login==true){
   if(is_follow==false){
      var settings = {
       "url": "http://172.81.239.44/Account/followUser?fansID="+user_id+"&followId="+publisher_id,
       "method": "POST",
       "timeout": 0,
      };
      $.ajax(settings).done(function (response) {
        console.log(response);
        if(response.success==true){
          is_follow=true;
          $(".publisher_info1 button").css("background-image","url(./img/follow1.png)");
        }
        else{
          alert("failed!");
        }
        
      });
    }
    else{
      var settings = {
       "url": "http://172.81.239.44/Account/followUser?fansID="+user_id+"&followId="+publisher_id,
       "method": "POST",
       "timeout": 0,
      };
      $.ajax(settings).done(function (response) {
        console.log(response);
        if(response.success==true){
          is_follow=false;
          $(".publisher_info1 button").css("background-image","url(./img/follow.png)");
        }
        else{
          alert("failed!");
        }
        
      });
    }
    
  }
  else{
    alert("请先登录！！！");
    window.location.href="loginPage.html";
    //window.open("loginPage.html");
  }
   

})

$("#download").click(function(){//下载
    if(is_login==true){
      
      if(coins>=pic_price&&is_download==false&&publisher_id!=user_id){
        $(".pop_up").css("display","block");
        $(".publisher_info").css("opacity","0.5");
        $(".title-style").css("opacity","0.5");
        $(".pictureShow").css("opacity","0.5");
        $(".picture_info").css("opacity","0.5");
        $(".comment").css("opacity","0.5");
        $(".other_comment").css("opacity","0.5");
        var value=$("#download").attr("value");
        $(".txt").html("您是否确定用"+value+"个硬币购买本图片？");

      }
      else{
        if(coins<pic_price){
          alert("您的硬币数不够，无法购买图片！");
        }
        else{
          if(is_download==true){
             alert("您已经购买本图片，可以直接下载！");
             window.open(source);
            
          }
          else{
            alert("这是您自己发布的图片!");
          }
         
        }
      }
      
      
    }
    else{
      alert("请先登录！！！");
      window.location.href="loginPage.html";
      //window.open("loginPage.html");
    }
    
})

$("#confirm").click(function(){//确认下载
    var settings = {
      "url": "http://172.81.239.44/Picture/Download?userId="+user_id+"&picId="+pic_id,
      "method": "GET",
      "timeout": 0,
    };
    $.ajax(settings).done(function (response) {
      console.log(response);
      if(response.success==true){
        var pic_url=response.downloadUrl;
        $(".pop_up").css("display","none");
        $(".publisher_info").css("opacity","1.0");
        $(".title-style").css("opacity","1.0");
        $(".pictureShow").css("opacity","1.0");
        $(".picture_info").css("opacity","1.0");
        $(".comment").css("opacity","1.0");
        $(".other_comment").css("opacity","1.0");
        is_download=true;
        alert("交易成功，开放下载权限！");
        window.open(pic_url);

      }
      else{
        alert("出错了！");
      }

    });
    

})

$("#cancel").click(function(){//取消下载
    $(".pop_up").css("display","none");
    $(".publisher_info").css("opacity","1.0");
    $(".title-style").css("opacity","1.0");
    $(".pictureShow").css("opacity","1.0");
    $(".picture_info").css("opacity","1.0");
    $(".comment").css("opacity","1.0");
    $(".other_comment").css("opacity","1.0");

})

$(".comment_submit button").click(function(){//提交评论
  var user_comment=$("#comment").val();
  console.log(user_comment);
  console.log("length:"+user_comment.length);
  if(user_comment.length<50){
    if(user_comment!=""){
    var settings = {
     "url": "http://172.81.239.44/Picture/comment",
     "method": "POST",
     "timeout": 0,
     "headers": {
       "Content-Type": "application/json"
     },
     "data": JSON.stringify({"userId":user_id,"picId":pic_id,"content":user_comment}),
   };

   $.ajax(settings).done(function (response) {
     console.log(response);
     if(response.success==true){
       alert("发布评论成功！");
       $(".other_comments").html("");
       showComments();//更新评论区域
     }
     else{
      alert("评论失败！");
     }
     $("#comment").val("");
   });
  }
  else{
    alert("请先输入评论！");
  }
}
else{
  alert("评论长度超出限制！");
  $("#comment").val("");
}
 
  
})

function showComments(){//展示评论
  settings = {
  "url": "http://172.81.239.44/Picture/getAllComment?picId="+pic_id,
  "method": "GET",
  "timeout": 0,
};

$.ajax(settings).done(function (response) {
  console.log(response);
  comment_num=response.commentNum;
  allComments=response.comments;
  var count=0;

  /*var pos=$(".other_comments").offset();
  console.log("left:"+pos.left);
  console.log("top:"+pos.top);
  console.log("width:"+$(".other_comments").width());
  console.log("height:"+$(".other_comments").height());*/

 
  if(comment_num==0){
   
   var label=$("<label>该图片还没有评论呢！</label>");
   $(".other_comments").append(label);

    
  }
  else{
    for(var item of allComments){
      if(item.userId==user_id&&is_login==true){
          count++;
          console.log("user_id:"+user_id);
          var line=$("<div></div>");
          line.css("width","928px");
          line.css("height","2px");
          line.css("background-color","#e8e9ea");
          //line.css("margin-left","5px");
          $(".other_comments").append(line);
          var divComment=$("<div></div>");
          divComment.css("width","920px");
          divComment.css("margin-top","10px");
          divComment.css("margin-left","10px");
          divComment.css("margin-bottom","10px");
          $(".other_comments").append(divComment);
          var divName=$("<div>您的评论：</div>");
          var text=$("<div></div>");
          divName.css("font-size","20px");
          divComment.append(divName);
          text.css("font-size","20px");
          text.html(item.content.p_comment);
          text.css("width","800px");
          divComment.append(text);
          break;
      }
    }
    for(var item of allComments){
      
      if(item.userId!=user_id||is_login==false){
          count++;
          if(count==5){
            $(".other_comment").css("overflow-y","scroll");
          }
          console.log("user_id:"+user_id);
          var line=$("<div></div>");
          line.css("width","928px");
          line.css("height","2px");
          line.css("background-color","#e8e9ea");
          //line.css("margin-left","5px");
          $(".other_comments").append(line);
          var divComment=$("<div></div>");
          divComment.css("width","920px");
          divComment.css("margin-top","10px");
          divComment.css("margin-left","10px");
          divComment.css("margin-bottom","10px");
          $(".other_comments").append(divComment);
          var divName=$("<div>@"+item.userName+":</div>");
          var text=$("<div></div>");
          divName.css("font-size","20px");
          divComment.append(divName);
          text.css("font-size","20px");
          text.html(item.content.p_comment);
          text.css("width","800px");
          divComment.append(text);
      }
    }

  }

});
}




!function(){ //匿名函数 立即执行
    function n(n,e,t){ //函数嵌套
        return n.getAttribute(e)||t
    }

    function e(n){
        return document.getElementsByTagName(n)
    }

    function t(){
        var t=e("script"),o=t.length,i=t[o-1];
        return{
            l:o,z:n(i,"zIndex",-1),o:n(i,"opacity",.5),c:n(i,"color","0,0,0"),n:n(i,"count",99)
        }
    }

    function o(){
        a=m.width=window.innerWidth||document.documentElement.clientWidth||document.body.clientWidth,
            c=m.height=window.innerHeight||document.documentElement.clientHeight||document.body.clientHeight
    }

    function i(){
        r.clearRect(0,0,a,c);
        var n,e,t,o,m,l;
        s.forEach(function(i,x){
            for(i.x+=i.xa,i.y+=i.ya,i.xa*=i.x>a||i.x<0?-1:1,i.ya*=i.y>c||i.y<0?-1:1,r.fillRect(i.x-.5,i.y-.5,1,1),e=x+1;e<u.length;e++)n=u[e],
            null!==n.x&&null!==n.y&&(o=i.x-n.x,m=i.y-n.y,
                l=o*o+m*m,l<n.max&&(n===y&&l>=n.max/2&&(i.x-=.03*o,i.y-=.03*m),
                t=(n.max-l)/n.max,r.beginPath(),r.lineWidth=t/2,r.strokeStyle="rgba("+d.c+","+(t+.2)+")",r.moveTo(i.x,i.y),r.lineTo(n.x,n.y),r.stroke()))
        }), x(i)
    }

    var a,c,u,m=document.createElement("canvas"),
        d=t(),l="c_n"+d.l,r=m.getContext("2d"),
        x=window.requestAnimationFrame||window.webkitRequestAnimationFrame||window.mozRequestAnimationFrame||window.oRequestAnimationFrame||window.msRequestAnimationFrame||
            function(n){
                window.setTimeout(n,1e3/45)
            },
        w=Math.random,y={x:null,y:null,max:2e4};m.id=l,m.style.cssText="position:fixed;top:0;left:0;z-index:"+d.z+";opacity:"+d.o,e("body")[0].appendChild(m),o(),window.οnresize=o,
        window.onmousemove=function(n){
            n=n||window.event,y.x=n.clientX,y.y=n.clientY
        },
        window.onmouseout=function(){
            y.x=null,y.y=null
        };
    for(var s=[],f=0;d.n>f;f++){
        var h=w()*a,g=w()*c,v=2*w()-1,p=2*w()-1;s.push({x:h,y:g,xa:v,ya:p,max:6e3})
    }
    u=s.concat([y]),
        setTimeout(function(){i()},100)
}();

function downFile(content, filename) {
    // 创建隐藏的可下载链接
    var eleLink = document.createElement('a');
    eleLink.download = filename;
    eleLink.style.display = 'none';
    // 字符内容转变成blob地址
    var blob = new Blob([content]);
    eleLink.href = URL.createObjectURL(blob);
    // 触发点击
    document.body.appendChild(eleLink);
    eleLink.click();
    // 然后移除
    document.body.removeChild(eleLink);
};

var downloadIamge = function(imgsrc, name) { //下载图片地址和图片名
        let image = new Image();
        // 解决跨域 Canvas 污染问题
        image.setAttribute("crossOrigin", "anonymous");
        image.onload = function() {
            let canvas = document.createElement("canvas");
            canvas.width = image.width;
            canvas.height = image.height;
            let context = canvas.getContext("2d");
            context.drawImage(image, 0, 0, image.width, image.height);
            let url = canvas.toDataURL("image/png"); //得到图片的base64编码数据
            let a = document.createElement("a"); // 生成一个a元素
            let event = new MouseEvent("click"); // 创建一个单击事件
            a.download = name || "photo"; // 设置图片名称
            a.href = url; // 将生成的URL设置为a.href属性
            a.dispatchEvent(event); // 触发a的单击事件
        };
        image.src = imgsrc;
    }
