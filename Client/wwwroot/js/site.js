// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Get the current path (without query params)
    var currentPath = window.location.pathname;

    // Remove active class from all nav items
    $(".nav-item").removeClass("active");

    // Loop through each nav item and check if it matches the path
    $(".nav-item").each(function () {
        var linkPath = $(this).attr("href");

        // Special case: Treat "/" as "/Home"
        if (currentPath === "/" && linkPath === "/Home") {
            $(this).addClass("active");
        } 
        // Standard case: match exact path or subpath
        else if (currentPath.startsWith(linkPath) && linkPath !== "/") {
            $(this).addClass("active");
        }
    });
});
