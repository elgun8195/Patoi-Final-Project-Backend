$(document).ready(function () {

  window.loopcounter = function (idWarp) {
    if (typeof idWarp != "undefined") {
      var date = $("." + idWarp).data("date");
      if (typeof date != "undefined") {
        var start = new Date(date.replace(/-/g, "/")),
          end = new Date(),
          diff = new Date(start - end),
          time = diff / 1000 / 60 / 60 / 24;

        var day = parseInt(time);
        var hour = parseInt(24 - ((diff / 1000 / 60 / 60) % 24));
        var min = parseInt(60 - ((diff / 1000 / 60) % 60));
        var sec = parseInt(60 - ((diff / 1000) % 60));

        counterDate(idWarp, day, hour, min, sec);

        var interval = setInterval(function () {
          if (sec == 0 && min != 0) {
            min--;
            sec = 60;
          }
          if (min == 0 && sec == 0 && hour != 0) {
            hour--;
            min = 59;
            sec = 60;
          }
          if (min == 0 && sec == 0 && hour == 0 && day != 0) {
            day--;
            hour = 23;
            min = 59;
            sec = 60;
          }
          if (min == 0 && sec == 0 && hour == 0 && day == 0) {
            clearInterval(interval);
          } else {
            sec--;
          }
          counterDate(idWarp, day, hour, min, sec);
        }, 1000);

        function counterDate(id, day, hour, min, sec) {
          if (time < 0) {
            day = hour = min = sec = 0;
          }
          $("." + id + " .counter-days").html(counterDoubleDigit(day));
          $("." + id + " .counter-hours").html(counterDoubleDigit(hour));
          $("." + id + " .counter-minutes").html(counterDoubleDigit(min));
          $("." + id + " .counter-seconds").html(counterDoubleDigit(sec));
        }
        function counterDoubleDigit(arg) {
          if (arg.toString().length <= 1) {
            arg = ("0" + arg).slice(-2);
          }
          return arg;
        }
      }
    }
  };
  $(window).on("scroll", function () {
    var scrolled = $(window).scrollTop();
    if (scrolled > 600) $(".go-top").addClass("active");
    if (scrolled < 600) $(".go-top").removeClass("active");
  });
  
  $(".go-top").on("click", function () {
    $("html, body").animate({ scrollTop: "0" }, 500);
  });
  //$(".input-counter").each(function () {
  //  var spinner = jQuery(this),
  //    input = spinner.find('input[type="text"]'),
  //    btnUp = spinner.find(".plus-btn"),
  //    btnDown = spinner.find(".minus-btn"),
  //    min = input.attr("min"),
  //    max = input.attr("max");
  //  btnUp.on("click", function () {
  //    var oldValue = parseFloat(input.val());
  //    if (oldValue >= max) {
  //      var newVal = oldValue;
  //    } else {
  //      var newVal = oldValue + 1;
  //    }
  //    spinner.find("input").val(newVal);
  //    spinner.find("input").trigger("change");
  //  });
  //  btnDown.on("click", function () {
  //    var oldValue = parseFloat(input.val());
  //    if (oldValue <= min) {
  //      var newVal = oldValue;
  //    } else {
  //      var newVal = oldValue - 1;
  //    }
  //    spinner.find("input").val(newVal);
  //    spinner.find("input").trigger("change");
  //  });
  //});

    $(document).on("keyup", "#search-inputt2", function () {
        let inputVal = $(this).val().trim();
        $("#searchlistt2 li").slice(1).remove();
        $.ajax({
            method: "get",
            url: "shop/Searchh?search=" + inputVal,
            success: function (res) {
                $("#searchlistt2").append(res);
            }
        })
    })
    $(document).on('click', '.categorys li a', function (e) {
        e.preventDefault();
        let category = $(this).attr('data-id');
        let products = $('.product-box');

        products.each(function () {
            if (category == $(this).attr('data-id')) {
                $(this).parent().fadeIn();
            }
            else {
                $(this).parent().hide();
            }
        })
        //if (category == 'all') {
        //    products.parent().fadeIn();
        //}
    })
});

//price range

const rangeInput = document.querySelectorAll(".range-input input"),
  priceInput = document.querySelectorAll(".price-input input"),
  range = document.querySelector(".slider .progress");
let priceGap = 1000;

priceInput.forEach((input) => {
  input.addEventListener("input", (e) => {
    let minPrice = parseInt(priceInput[0].value),
      maxPrice = parseInt(priceInput[1].value);
    console.log(minPrice.value);
    if (maxPrice - minPrice >= priceGap && maxPrice <= rangeInput[1].max) {
      if (e.target.className === "input-min") {
        rangeInput[0].value = minPrice;
        range.style.left = (minPrice / rangeInput[0].max) * 100 + "%";
      } else {
        rangeInput[1].value = maxPrice;
        range.style.right = 100 - (maxPrice / rangeInput[1].max) * 100 + "%";
      }
    }
  });
});

rangeInput.forEach((input) => {
  input.addEventListener("input", (e) => {
    let minVal = parseInt(rangeInput[0].value),
      maxVal = parseInt(rangeInput[1].value);

    if (maxVal - minVal < priceGap) {
      if (e.target.className === "range-min") {
        rangeInput[0].value = maxVal - priceGap;
      } else {
        rangeInput[1].value = minVal + priceGap;
      }
    } else {
      priceInput[0].value = minVal;
      priceInput[1].value = maxVal;
      range.style.left = (minVal / rangeInput[0].max) * 100 + "%";
      range.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";
    }
  });
});

//price range
let inner = document.getElementById("inner");
let optioninner = document.getElementById("option-inner");
let mexpand2 = document.getElementById("mexpand2");
let mexpand3 = document.getElementById("mexpand3");
let mexpand4 = document.getElementById("mexpand4");
let mmenu = document.getElementById("meanmenu-reveal");
let navbarnav = document.getElementById("navbar-nav");
let dropdown1 = document.getElementById("dmenu1");
let dropdown2 = document.getElementById("dmenu2");
let dropdown3 = document.getElementById("dmenu3");
let dropdown4 = document.getElementById("dmenu4");

mexpand2.addEventListener("click", () => {
  if (dropdown2.style.display === "none") {
    dropdown2.style.display = "block";
  } else {
    dropdown2.style.display = "none";
  }
});
mexpand3.addEventListener("click", () => {
  if (dropdown3.style.display === "none") {
    dropdown3.style.display = "block";
  } else {
    dropdown3.style.display = "none";
  }
});
mexpand4.addEventListener("click", () => {
  if (dropdown4.style.display === "none") {
    dropdown4.style.display = "block";
  } else {
    dropdown4.style.display = "none";
  }
});
inner.addEventListener("click", () => {
  optioninner.classList.toggle("active");
});
mmenu.addEventListener("click", () => {
  mmenu.classList.toggle("meanclose");

  if (navbarnav.style.display === "none") {
    navbarnav.style.display = "block";
  } else {
    navbarnav.style.display = "none";
  }
});
// Product Detaisl Carousel
const imgs = document.querySelectorAll(".img-select a");
const imgBtns = [...imgs];
let imgId = 1;

imgBtns.forEach((imgItem) => {
  imgItem.addEventListener("click", (event) => {
    event.preventDefault();
    imgId = imgItem.dataset.id;
    slideImage();
  });
});

function slideImage() {
  const displayWidth = document.querySelector(
    ".img-showcase img:first-child"
  ).clientWidth;

  document.querySelector(".img-showcase").style.transform = `translateX(${
    -(imgId - 1) * displayWidth
  }px)`;
}

window.addEventListener("resize", slideImage);
// Product Detaisl Carousel


//
const countriesList = document.getElementById("countries");
let countries;

countriesList.addEventListener("change", newCountrySelection);

function newCountrySelection(event) {
  displayCountryInfo(event.target.value);
}

const link = "https://restcountries.com/v2/all";

fetch(link)
  .then((res) => res.json())
  .then((data) => initialize(data))
  .catch((err) => console.log("Error:", err));

function initialize(countriesData) {
  countries = countriesData;
  let options = "";
  countries.forEach(
    (country) =>
      (options += `<option value="${country.alpha3Code}">${country.name}</option>`)
  );

  countriesList.innerHTML = options;

  countriesList.selectedIndex = Math.floor(
    Math.random() * countriesList.length
  );
  displayCountryInfo(countriesList[countriesList.selectedIndex].value);
}

function displayCountryInfo(countryByAlpha3Code) {
  const countryData = countries.find(
    (country) => country.alpha3Code === countryByAlpha3Code
  );
  document.querySelector("#flag-container img").src = countryData.flag;
  document.querySelector(
    "#flag-container img"
  ).alt = `Flag of ${countryData.name}`;
}

//////////////////////////////////4

const countries1List = document.getElementById("countries1");
let countries1; 
countries1List.addEventListener("change", newCountrySelectione);

function newCountrySelectione(event) {
  displayCountryInfoe(event.target.value);
}

const link1="https://restcountries.com/v2/all";

fetch(link1)
.then(res => res.json())
.then(data => initializ(data))
.catch(err => console.log("Error:", err));

function initializ(countries1Data) {
  countries1 = countries1Data;
  let option = "";
 
  countries1.forEach(countr => option+=`<option value="${countr.alpha3Code}">${countr.name}</option>`);
  countries1List.innerHTML = option;
  countries1List.selectedIndex = Math.floor(Math.random()*countries1List.length);
  displayCountryInfoe(countries1List[countries1List.selectedIndex].value);
}

function displayCountryInfoe(countryByAlpha3Cod) {
  const countryDatae = countries1.find(countr => countr.alpha3Code === countryByAlpha3Cod);
  document.querySelector("#flag-containere img").src = countryDatae.flag;
  document.querySelector("#flag-containere img").alt = `Flag of ${countryDatae.name}`;  

}

////////////////////////////

