import * as ko from "knockout"
import { KoTools } from "./koTools"
import { AppViewModel, Item } from "./AppViewModel"


window.onload = function () {
    KoTools.Init();
    ko.applyBindings(new AppViewModel());

    $(window).focus(function () {
        $("#input").focus();
    });
    $("#input").focus();
};