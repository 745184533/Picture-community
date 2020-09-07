<template>
    <div>
      <div class="title-style">
			<div class="nav-style">
				<div class="navbar-nav">
					<ul class="nav nav-tabs" role="tablist">
						<li role="presentation" style="margin: 0 60px 0 0;" ><span style="height: 40px;line-height: 40px;font-weight: bold;font-size: 18px;">图片社区</span></li>
					    <li role="presentation" class="active" style="margin: 9px 60px 0 0;" ><a href="#photo" aria-controls="home" role="tab" data-toggle="tab" style="color: black;">照片</a></li>
					    <li role="presentation" style="margin: 9px 60px 0 0;" ><a href="#illustration" aria-controls="home" role="tab" data-toggle="tab" style="color: black;">插画</a></li>
						<li role="presentation" style="margin: 9px 60px 0 0;"><a href="#vector" aria-controls="home" role="tab" data-toggle="tab" style="color: black;">向量</a></li>
						<li role="presentation" class="dropdown" style="margin: 0 60px 0 650px;">
						    <button class="btn btn-primary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown"
						      aria-haspopup="true" aria-expanded="true">探索<span class="caret"></span>
						    </button>
							<table class="dropdown-menu" aria-labelledby="dropdownMenu1">
								<tr>
									<th>Media</th>
									<th>Community</th>
									<th>About</th>
								</tr>
								<tr>
									<td><a>小编精选</a></td>
									<td><a>博客</a></td>
									<td><a>服务条款</a></td>
								</tr>
								<tr>
									<td><a>Popular Images</a></td>
									<td><a>论坛</a></td>
									<td><a>隐私政策</a></td>
								</tr>
								<tr>
									<td><a>Popular Videos</a></td>
									<td><a>Artists</a></td>
									<td><a>关于我们</a></td>
								</tr>
							</table>
						</li>
						<li style="margin: 9px 60px 0 0;"><router-link to='/login'>登录</router-link></li>
						<li style="margin: 9px 60px 0 0;"><router-link to='/register'>注册</router-link></li>
						<li style="margin: 9px 20px 0 0;"><a href="" style="color: black;">
							<svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-upload" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
							    <path fill-rule="evenodd" d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5z"/>
							    <path fill-rule="evenodd" d="M7.646 1.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708L8.5 2.707V11.5a.5.5 0 0 1-1 0V2.707L5.354 4.854a.5.5 0 1 1-.708-.708l3-3z"/>
						    </svg>上传</a></li>
					</ul>
				</div>
			</div>
		</div>
		<div class="tab-content">
			<div role="tabpanel" class="tab-pane active" id="photo">
				<div class="search-style1">
					<h1 style="padding-top: 100px;padding-left:530px;color: white;align-content: center;">免费正版高清图片素材库</h1>
					<h4 style="margin-top:30px; padding-left: 402px;color: white;">图片社区拥有超过许多优质图片素材，让你轻松应对各种设计场景</h4>
					<div class="search" style="display: flex;">
						<div style="height: 60px; width: 60px; background-color: #fff;margin-left: 500px;margin-top: 50px;">
							<svg width="50px" height="50px" viewBox="0 0 16 16" class="bi bi-search" style="padding-left: 10px; padding-top:15px;" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
								<path fill-rule="evenodd" d="M10.442 10.442a1 1 0 0 1 1.415 0l3.85 3.85a1 1 0 0 1-1.414 1.415l-3.85-3.85a1 1 0 0 1 0-1.415z"/>
								<path fill-rule="evenodd" d="M6.5 12a5.5 5.5 0 1 0 0-11 5.5 5.5 0 0 0 0 11zM13 6.5a6.5 6.5 0 1 1-13 0 6.5 6.5 0 0 1 13 0z"/>
							  </svg>
						</div>			
						<form action="" method="post">
							<input type="search" placeholder="照片" style="outline: none; width: 440px; height: 60px; font-size: 20px; margin-top: 50px;border: none;"/>
						</form>
					</div>
				</div>
			</div>
			<div role="tabpanel" class="tab-pane" id="illustration">
				<div class="search-style2">
					<h1 style="padding-top: 100px;padding-left:530px;color: white;align-content: center;">免费正版高清图片素材库</h1>
					<h4 style="margin-top:30px; padding-left: 402px;color: white;">图片社区拥有超过许多优质图片素材，让你轻松应对各种设计场景</h4>
					<div class="search" style="display: flex;">
						<div style="height: 60px; width: 60px; background-color: #fff;margin-left: 500px;margin-top: 50px;">
							<svg width="50px" height="50px" viewBox="0 0 16 16" class="bi bi-search" style="padding-left: 10px; padding-top:15px;" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
								<path fill-rule="evenodd" d="M10.442 10.442a1 1 0 0 1 1.415 0l3.85 3.85a1 1 0 0 1-1.414 1.415l-3.85-3.85a1 1 0 0 1 0-1.415z"/>
								<path fill-rule="evenodd" d="M6.5 12a5.5 5.5 0 1 0 0-11 5.5 5.5 0 0 0 0 11zM13 6.5a6.5 6.5 0 1 1-13 0 6.5 6.5 0 0 1 13 0z"/>
							  </svg>
						</div>			
						<form action="" method="post">
							<input type="search" placeholder="插画" style="outline: none; width: 440px; height: 60px; font-size: 20px; margin-top: 50px;border: none;"/>
						</form>
					</div>
				</div>
			</div>
			<div role="tabpanel" class="tab-pane" id="vector">
				<div class="search-style3">
					<h1 style="padding-top: 100px;padding-left:530px;color: white;align-content: center;">免费正版高清图片素材库</h1>
					<h4 style="margin-top:30px; padding-left: 402px;color: white;">图片社区拥有超过许多优质图片素材，让你轻松应对各种设计场景</h4>
					<div class="search" style="display: flex;">
						<div style="height: 60px; width: 60px; background-color: #fff;margin-left: 500px;margin-top: 50px;">
							<svg width="50px" height="50px" viewBox="0 0 16 16" class="bi bi-search" style="padding-left: 10px; padding-top:15px;" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
								<path fill-rule="evenodd" d="M10.442 10.442a1 1 0 0 1 1.415 0l3.85 3.85a1 1 0 0 1-1.414 1.415l-3.85-3.85a1 1 0 0 1 0-1.415z"/>
								<path fill-rule="evenodd" d="M6.5 12a5.5 5.5 0 1 0 0-11 5.5 5.5 0 0 0 0 11zM13 6.5a6.5 6.5 0 1 1-13 0 6.5 6.5 0 0 1 13 0z"/>
							  </svg>
						</div>			
						<form action="" method="post">
							<input type="search" placeholder="向量" style="outline: none; width: 440px; height: 60px; font-size: 20px; margin-top: 50px;border: none;"/>
						</form>
					</div>
				</div>
			</div>	
		</div>
    </div>
</template>

<script>
export default {
  
}
</script>

<style scoped>

.title-style{
	height: 40px;
	width: 100%;
	background-color: #66AFE9 ;
}
.nav-style{
	margin: auto 0;
	margin-left: 60px;
	float: left;
}

.nav-tabs {
    border-bottom: none;
}
.dropdown{
	height: 40px;
}
.btn.btn-primary.dropdown-toggle{
	height: 40px;
	background-color: #66AFE9;
}
.body-style{
	width: 100%;
	margin-top: 20px;
}
.btn.btn-primary.dropdown-toggle.two{
	border: none;
	height: 30px;
	background-color: #FFFFFF;
}
.dropdown-style{
	float: left;
	margin-left: 70px;
	height: 1000px;
	border-right: #555555 1px solid;
	width: 172px;
}

.content-style{
	float:left;
	
}
.content-header{
	border-bottom: #555555 1px solid;
	height: 100px;
}
.text-size{
	margin-left: 30px;
}
.body-title{
	height: 30px;
	margin-left: 30px;
	margin-top: 10px;
	border: 1px solid #737373;
	line-height: 30px;
}
.table-style{
	height: 400px;
	width: 1000px;
	margin-left: 30px;
	margin-top: 10px;
	border: 1px #737373 solid;
}
.table-header{
	height: 20px;
	width: 100px;
	float: left;
	margin-left: 30px;
	margin-top: 15px;
	border-left: #000000 4px solid;
}
.select-block{
	float: left;
	margin-left: 600px;
	margin-top: 10px;
	display: inline-block;
}
.dropdown-three{
	float: right;
	margin-right: 20px;
}
.btn.btn-primary.dropdown-toggle.three{
	border: none;
	height: 20px;
	margin-top: 0px;
	background-color: #FFFFFF;
}
.table.table-striped.table-bordered{
	margin-top: 40px;
	margin-left: 20px;
	width: 970px;
}
.pagination{
	margin: 0 auto;
	margin-left: 300px;
}
table{
	width: 390px;
	background-color: #101010;
}
th{
	width: 130px;
	padding: 10px;
	color: white;
	background-color: #222222;
}
td{
	width: 130px;
	padding: 10px;
	color: #D9EDF7;
	background-color: #222222;
}
td>a{
	color: #CCCCCC;
}
td>a:hover{
	text-decoration: none;
	color: white;
}
.search-style1{
	width: 100%;
  height: 430px;
  background-image: url(../assets/photo.jpg);
}
.search-style2{
	width: 100%;
  height: 430px;
  background-image: url(../assets/bg1.jpg);
}
.search-style3{
	width: 100%;
  height: 430px;
  background-image: url(../assets/bg2.jpg);
}
.active{
    color:#D9EDF7;
}
input::-webkit-input-placeholder{text-align: left;}
input{
	outline: none;
}
</style>
