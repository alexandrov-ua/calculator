window.onload = function () {
    function processResponse(response) {
        if (response.isSuccessful) {
            return response.result;
        }
        var error = response.diagnostics[0];
        return makeArrow(error.span) + " " + getErrorMessage(error.kind, error.parameters);
    }

    function getErrorMessage(kind, params) {
        switch (kind) {
            case "UnexpectedToken":
                return "Expected: " + params[0] + ". But found: " + params[1];
            default:
                return "Error: " + kind;
        }
    }

    function makeArrow(span) {
        var res = "";
        for (var i = 0; i < span.start + 1; i++) {
            res = res + "-";
        }
        for (var j = 0; j < span.length; j++) {
            res = res + "^";
        }
        return res;
    }

    function showHelp(items) {
        items.push("");
        items.push("");
        items.push("");
    }

    //function processInput(self, input) {
    //    self.items.push(new Item(">" + this.input()));
    //    switch (input.trim()) {
    //        case "#help":
    //            showHelp(self.items);
    //            break;
    //        case "#cls":
    //            self.items.clear();
    //            break;
    //        default:
    //            {
    //                var item = new Item("...");
    //                self.items.push(item);
    //                this.client.calculate(this.input(),
    //                    function (response) {
    //                        item.val(processResponse(response));
    //                        if (!response.isSuccessful) {
    //                            item.isError(true);

    //                        }
    //                    });

    //            }
    //    }
    //    this.input("");
    //}

    function Item(text) {
        this.val = ko.observable(text);
        this.isError = ko.observable(false);
    }

    function AppViewModel() {
        this.input = ko.observable("");
        this.items = ko.observableArray([]).extend({ scrollFollow: '#container' });
        this.client = new ApiClient();
        this.calculate = function () {
            var self = this;
            this.items.push(new Item(">" + this.input()));
            var item = new Item("...");
            self.items.push(item);
            this.client.calculate(this.input(),
                function (response) {
                    item.val(processResponse(response));
                    if (!response.isSuccessful) {
                        item.isError(true);

                    }
                });
            this.input("");
            //processInput(self, self.input());
        };

        this.onEnterKey = function () {
            this.calculate();
        };
    }

    ko.applyBindings(new AppViewModel());

    $(window).focus(function () {
        $("#input").focus();
    });
    $("#input").focus();
};

