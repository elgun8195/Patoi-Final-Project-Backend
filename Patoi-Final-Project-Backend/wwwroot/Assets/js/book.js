searchForm = document.querySelector('.search-form');
let items=document.querySelectorAll(".buton")




items.forEach(item=>{
    item.classList.remove("active")
    item.addEventListener('click',function(){
        items.forEach(item=>{
            item.firstElementChild.classList.remove("active")
        })
        if(!this.firstElementChild.classList.contains("active")){
            this.firstElementChild.classList.add("active")
        }
    })
})


document.querySelector('#search-btn').onclick = () => {
    searchForm.classList.toggle('active');
}

window.onscroll = () => {

    searchForm.classList.remove('active');

    if (window.scrollY > 80) {
        document.querySelector('.header .header-2').classList.add('active');
    } else {
        document.querySelector('.header .header-2').classList.remove('active');
    }

}

window.onload = () => {

    if (window.scrollY > 80) {
        document.querySelector('.header .header-2').classList.add('active');
    } else {
        document.querySelector('.header .header-2').classList.remove('active');
    }

    fadeOut();

}

function loader() {
    document.querySelector('.loader-container').classList.add('active');
}

function fadeOut() {
    setTimeout(loader, 2000);
}



var elements = document.getElementsByClassName("column");
// var cards=document.getElementsByClassName("card");
// Declare a loop variable
var i;

// List View
function listView() {
  for (i = 0; i < elements.length; i++) {
    elements[i].style.width = "100%";
    elements[i].lastElementChild.firstElementChild.lastElementChild.style.width="20rem";

    
  }
  $(".cards").addClass("card-display")
  
}

// Grid View
function gridView() {
  for (i = 0; i < elements.length; i++) {
    elements[i].style.width = "45%";
    elements[i].lastElementChild.firstElementChild.lastElementChild.style.width="70%";
  }
  $(".cards").removeClass("card-display")
  
}
// Grid View
function gridViewThree() {
    for (i = 0; i < elements.length; i++) {
      elements[i].style.width = "31%";
      elements[i].lastElementChild.firstElementChild.lastElementChild.style.width="100%";

    }
    $(".cards").removeClass("card-display")
   
}






		
    






    // Sidebar on right (
 

  function openNav() {
    document.getElementById("mySidenav").style.width = "400px";
  }
  
  function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
  }


   //back to top

   $(window).scroll(function(){
    if($(window).scrollTop()<500){
        $('#rocketmeluncur').slideUp(600);
    }else{
        $('#rocketmeluncur').slideDown(600);
    }
    var ftrocketmeluncur = $("#ft")[0] ? $("#ft")[0] : $(document.body)[0];
    var scrolltoprocketmeluncur = $('rocketmeluncur');
var viewPortHeightrocketmeluncur = parseInt(document.documentElement.clientHeight);
var scrollHeightrocketmeluncur = parseInt(document.body.getBoundingClientRect().top);
var basewrocketmeluncur = parseInt(ftrocketmeluncur.clientWidth);
var swrocketmeluncur = scrolltoprocketmeluncur.clientWidth;
if (basewrocketmeluncur < 200) {
var leftrocketmeluncur = parseInt(fetchOffset(ftrocketmeluncur)['left']);
leftrocketmeluncur = leftrocketmeluncur < swrocketmeluncur ? leftrocketmeluncur * 2 - swrocketmeluncur : leftrocketmeluncur;
scrolltoprocketmeluncur.style.left = ( basewrocketmeluncur + leftrocketmeluncur ) + 'px';
} else {
scrolltoprocketmeluncur.style.left = 'auto';
scrolltoprocketmeluncur.style.right = '10px';
}
})

$('#rocketmeluncur').click(function(){
    $("html, body").animate({ scrollTop: '0px',display:'none'},{
            duration: 150,  
            easing: 'linear'
        });
    
    var self = this;
    this.className += ' '+"launchrocket";
    setTimeout(function(){
      self.className = 'showrocket';
    },800)
});