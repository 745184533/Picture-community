
localStorage.setItem('userId', '123123');
var userid = localStorage.getItem("userId");
console.log(userid);

/*点击弹出按钮*/
function popBox() {
    var popBox = document.getElementById("popBox_upload");
    var popLayer = document.getElementById("popLayer");
    popBox.style.display = "block";
    popLayer.style.display = "block";
}

/*点击关闭按钮*/
function closeBox() {
    var popBox = document.getElementById("popBox_upload");
    var popLayer = document.getElementById("popLayer");
    popBox.style.display = "none";
    popLayer.style.display = "none";
}

/*上传图片并展示*/
function showImg(){
    //判断是否支持FileReader
    if (window.FileReader) {
        var re = new FileReader();
    } else {
        alert("您的设备不支持图片预览功能，如需该功能请升级您的设备！");
    }
    //获取文件
    var file =  document.getElementById('img_file').files[0];
    console.log(file);
    var imageType = /^image\//;
    if(!imageType.test(file.type)){
        alert("提交失败！请选择图片！");
        return;
    }

    re.readAsDataURL(file);
    re.onload = function(re){
        $('#img_id').attr("src", re.target.result);//图片路径设置为读取的图片
    }
	console.log("展示");
    showTag();
}
var picid1;
function showTag(){
    var file =  document.getElementById('img_file').files[0];

    var form = new FormData();
    form.append("userId", "1");
    form.append("", file);
    $.ajax({
        "url": "http://172.81.239.44/Account/Upload1",
        "method": "POST",
        "timeout": 0,
        "processData": false,
        "mimeType": "multipart/form-data",
        "contentType": false,
        "data": form,
        /*"headers":{
            "Authorization":"Bearer "+"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJiN2JhYjJmNS1hZGEyLTQxZmItYmE3My02YjY1OWE2N2UzYTUiLCJuYW1lIjoicm9vdCIsIm5iZiI6MTYwMDQxOTI0NCwiZXhwIjoxNjAwNDE5ODQ0LCJpc3MiOiJqd3RJc3N1ZXJ0ZXN0IiwiYXVkIjoiand0QXVkaWVuY2V0ZXN0In0.zFGry7hviglkwLrJyGlPU-tISpdWqmHQAIjOTJh53NY",
        },*/
        success:function(data){
            console.log(data);
			console.log("成功");
            var obj=eval('('+data+')');
            var arr=obj.tags;
            console.log(arr);
			picid1=obj.pictureId;
            var container = document.querySelector('.tag_show');
            arr.forEach(function(item,index){
                var i=index+1;
                console.log(i);
                var div=document.createElement('div');
                div.setAttribute("class","radio-label")
                var myCheckBox=document.createElement('input');
                myCheckBox.setAttribute("type","checkbox");
                myCheckBox.setAttribute("name","tag");
                myCheckBox.setAttribute("id","tag"+i);
                var myLabel=document.createElement('label');
                myLabel.setAttribute("for","tag"+i);
                myLabel.innerText=item;
                div.appendChild(myCheckBox);
                div.appendChild(myLabel);
                container.appendChild(div);
            })
        }
    });

    /*arr.forEach(function (item) {
        var div = document.createElement('div');
        div.setAttribute("class", "show");
        var img = document.createElement('img');
        img.src = item.picUrl;
        img.setAttribute("class", "content");
        div.appendChild(img);
        container.appendChild(div);
    })*/
}


/*监听遵循协议按钮，若两者都选，则提交按钮可用*/
function checkAgree(){
    if(document.getElementById('agree_1').checked==true &&
        document.getElementById('agree_2').checked==true)
        document.getElementById("submit").removeAttribute("disabled");
    else
        document.getElementById("submit").setAttribute("disabled", true);
}

/*提交后检查,是否为图片,标签1是否为空,图片简介是否为空,定价是否为数字且合理,标签是否重复*/
function check(){
    var file =  document.getElementById('img_file').files[0];
    var imageType = /^image\//;
    if(!imageType.test(file.type)){
        alert("提交失败！请选择图片！\n并请检查其他必填项是否填写！");
        return;
    }
    var intro_pic=$("#intro").val();
    var tag=$("#tag1").val();
    var tag_1=$("#tag2").val();
    var tag_2=$("#tag3").val();
    var price_pic=$("#price").val();
	console.log(tag,tag_1,tag_2);
    if(intro_pic == null || intro_pic == ""){
        alert("提交失败！图片简介不能为空！\n并请检查其他必填项是否填写！");
        return;
    }
    else if(tag == null || tag ==""){
        alert("提交失败！标签1不能为空！\n并请检查其他必填项是否填写！");
        return;
    }
    else if(isNaN(price_pic) || price_pic>10000 || price_pic == "" || price_pic<0){
        alert("提交失败！定价请填入数字,且范围在0-10000之间！")
    }
    else{
        var form = new FormData();
        form.append("tag", tag);
        form.append("tag1", tag_1);
        form.append("tag2", tag_2);
        form.append("userId", "2");
        form.append("p_info", intro_pic);
        
        form.append("price", price_pic);
		form.append("", file);
		form.append("pictureId",picid1);
		console.log(picid1);
        //console.log(fileInput.files[0]);
        $.ajax({
            "url": "http://172.81.239.44/Account/Upload2",
            "method": "POST",
            "timeout": 0,
            "processData": false,
            "mimeType": "multipart/form-data",
            "contentType": false,
            "data": form,
            success:function(data){
                alert("提交成功！");
            },
			error:function(e){
				console.log(e);
			}
        })
        closeBox();
    }
}



/*相似图片上传，全部类似的js部分*/
/*点击弹出按钮*/
function popBox_search() {
    var popBox = document.getElementById("popBox_search");
    var popLayer = document.getElementById("popLayer");
    popBox.style.display = "block";
    popLayer.style.display = "block";
}

/*点击关闭按钮*/
function closeBox_search() {
    var popBox = document.getElementById("popBox_search");
    var popLayer = document.getElementById("popLayer");
    popBox.style.display = "none";
    popLayer.style.display = "none";
}
/*上传图片并展示*/
function showImg_search(){
    //判断是否支持FileReader
    if (window.FileReader) {
        var re = new FileReader();
    } else {
        alert("您的设备不支持图片预览功能，如需该功能请升级您的设备！");
    }
    //获取文件
    var file =  document.getElementById('img_file_search').files[0];
    console.log(file);
    var imageType = /^image\//;
    if(!imageType.test(file.type)){
        alert("提交失败！请选择图片！");
        return;
    }

    re.readAsDataURL(file);
    re.onload = function(re){
        $('#img_id_search').attr("src", re.target.result);//图片路径设置为读取的图片
    }
}
/*提交后检查，是否上传为图片*/
function check_search(){
    var file =  document.getElementById('img_file_search').files[0];
    //console.log(file);
    var imageType = /^image\//;
    if(!imageType.test(file.type)){
        alert("提交失败！请选择图片！");
        return;
    }
    else{
        var file =  document.getElementById('img_file_search').files[0];
        var form=new FormData();
        form.append("", file);
        console.log(file);
        $.ajax({
            "url": "http://172.81.239.44/Account/SimilarPicture",
            "method": "POST",
            "timeout": 0,
            "processData": false,
            "mimeType": "multipart/form-data",
            "contentType": false,
            "data": form,
            success: function (data) {
                console.log(data);
                var obj = eval('(' + data + ')');
                var arr = obj.picList;
                var array=[];
                //console.log(arr);
                arr.forEach(function(item,index) {
                   console.log(item.picUrl);
                   array.push(item.picUrl);
                })
                localStorage.pic_Lists=array;
                console.log(array[1]);
            },
            error:function (e){
                console.log(e);
            }
        })
        localStorage.setItem("search_type","searchSimilar");
        closeBox_search();
        window.location.href="showPicture.html";
    }
}