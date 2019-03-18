ko.extenders.scrollFollow = function (target, selector) {
    target.subscribe(function (newval) {
        var el = document.querySelector(selector);

        if (el.scrollTop == el.scrollHeight - el.clientHeight) {
            setTimeout(function () { el.scrollTop = el.scrollHeight - el.clientHeight; }, 0);
        }
    });

    return target;
};

ko.bindingHandlers.enterkey = {
    init: function (element, valueAccessor, allBindings, viewModel) {
        var callback = valueAccessor();
        $(element).keypress(function (event) {
            var keyCode = (event.which ? event.which : event.keyCode);
            if (keyCode === 13) {
                callback.call(viewModel);
                return false;
            }
            return true;
        });
    }
};
