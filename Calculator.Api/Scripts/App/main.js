window.onload = function () {
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
        this.items = ko.observableArray([]);
        this.calculate = function () {
            var self = this;
            this.items.push(new Item(this.input()));
            $.post("api/evaluator/evaluate",
                JSON.stringify({ expression: this.input() }),
                function (response) {
                    self.output(response.result);
                    self.items.push(new Item(response.result));
                });

            this.output();
        };

        this.keypressed = function (data, event) {
            if (event.which == 13) {
                this.calculate();
            }
        }
    }
    ko.applyBindings(new AppViewModel());
}

