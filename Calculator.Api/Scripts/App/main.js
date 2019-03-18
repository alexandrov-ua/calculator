window.onload = function () {
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

    $.ajaxSetup({
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Accept", "application/json");
            xhr.setRequestHeader("Content-Type", "application/json");
        }
    });

    function Item(text) {
        self.val = text;
    }


    function AppViewModel() {
        this.input = ko.observable("asd");
        this.output = ko.observable("");
        this.items = ko.observableArray([]).extend({ scrollFollow: '#container' });
        this.calculate = function () {
            var self = this;
            this.items.push(new Item(">" + this.input()));
            $.post("api/evaluator/evaluate",
                JSON.stringify({ expression: this.input() }),
                function (response) {
                    self.output(response.result);
                    self.items.push(new Item(response.result));
                });

            this.output();
        };

        this.onEnterKey = function () {
            this.calculate();
        }
    }
    ko.applyBindings(new AppViewModel());
    $("#input").focus();
}

