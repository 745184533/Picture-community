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
    if(!/\.(jpg)$/.test(file.name)){
        alert("系统只接受jpg格式的图片！请重新上传！");
        $('#img_id').attr("src", '');
        return;
    }

    re.readAsDataURL(file);
    re.onload = function(re){
        $('#img_id').attr("src", re.target.result);//图片路径设置为读取的图片
    }
}

/*监听遵循协议按钮，若两者都选，则提交按钮可用*/
function checkAgree(){
    if(document.getElementById('agree_1').checked==true &&
        document.getElementById('agree_2').checked==true)
        document.getElementById("submit").removeAttribute("disabled");
    else
        document.getElementById("submit").setAttribute("disabled", true);
}

/*提交后检查，图片格式是否为jpg,标签1是否为空*/
function check(){
    var file =  document.getElementById('img_file').files[0];
    if(!/\.(jpg)$/.test(file.name)){
        alert("提交失败！图片格式必须为jpg！");
        return;
    }
    var tag=$("#tag1").val();
    if(tag == null || tag ==""){
        alert("提交失败！标签1不能为空！");
        return;
    }
    else{
        alert("提交成功！");
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
    if(!/\.(jpg)$/.test(file.name)){
        alert("系统只接受jpg格式的图片！请重新上传！");
        $('#img_id_search').attr("src", '');
        return;
    }

    re.readAsDataURL(file);
    re.onload = function(re){
        $('#img_id_search').attr("src", re.target.result);//图片路径设置为读取的图片
    }
}

/*提交后检查，图片格式是否为jpg*/
function check_search(){
    var file =  document.getElementById('img_file_search').files[0];
    if(!/\.(jpg)$/.test(file.name)){
        alert("提交失败！图片格式必须为jpg！");
        return;
    }
    else{
        alert("提交成功！");
        closeBox_search();
    }
}