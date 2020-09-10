// JavaScript Document$(function(){
state.onclick = function()
                {
                    var text = this.innerHTML;
                    if(text == "-")
                    {
                        this.innerHTML = "+";
                        uploadpic.style.display = "none";
                    }
                    else
                    {
                        this.innerHTML = "-";
                        uploadpic.style.display = "block";
                    }
                }
 var $ = function(ids)
                {
                    return document.getElementById(ids);
                }

                var tabs = $("tabs").getElementsByTagName("li");
                var state = $("state");
                var uploadpic = $("uploadpic");
                var ul = uploadpic.getElementsByTagName("ul");
for(var i = 0; i < tabs.length; i++)
                {
                    tabs[i].index = i;
                    tabs[i].onmouseover = function()
                    {
                        for(var i = 0; i < tabs.length; i++)
                        {
                            tabs[i].className = ul[i].className = "";
                        }
                        this.className = ul[this.index].className = "active";
                    `

                }