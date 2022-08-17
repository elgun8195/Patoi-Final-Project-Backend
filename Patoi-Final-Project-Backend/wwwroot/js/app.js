$(document).ready(function () {
    $(window).on("scroll", function () {
        if ($(this).scrollTop() > 120) {
            $(".navbar-area").addClass("is-sticky");
        } else {
            $(".navbar-area").removeClass("is-sticky");
        }
    });

    $(".others-option .search-icon i").on("click", function () {
        $(".search-overlay").toggleClass("search-overlay-active");
    });
    $(".search-overlay-close").on("click", function () {
        $(".search-overlay").removeClass("search-overlay-active");
    });
    $(document).on("keyup", "#search-inputt", function () {
        let inputVal = $(this).val().trim();
        $("#searchlistt li").slice(1).remove();
        $.ajax({
            method: "get",
            url: "shop/Search?search=" + inputVal,
            success: function (res) {
                $("#searchlistt").append(res);
            }
        })
    })


});
const prices = document.querySelectorAll("#total-price");
const cem = document.getElementById("cem");
const shipping = document.getElementById("shipping");
const final = document.getElementById("final");

var sum = 0;
prices.forEach(
    (x) =>
        // console.log(parseInt(x.textContent.substring(1)))
        (sum += parseInt(x.textContent.substring(1)))
);
cem.innerHTML = "$" + sum;
let faiz = (sum * 7) / 100;
shipping.innerHTML = "$" + faiz;
final.innerHTML = "$" + (sum + faiz);
