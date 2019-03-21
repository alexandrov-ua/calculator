import * as ko from "knockout"
import { ApiClient } from "./ApiClient"

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

class Item {
    constructor(text) {
        this.val = ko.observable(text);
        this.isError = ko.observable(false);
    }
}

class AppViewModel {
    constructor() {
        this.input = ko.observable("");
        this.items = ko.observableArray([new Item("Simple math expressions evaluator. For help type: #help")]).extend({ scrollFollow: '#container' });
        this.client = new ApiClient();
    }

    onEnterKey() {
        let input = this.input();
        this.processInput(input);
    }

    processInput(input) {
        this.items.push(new Item(">" + input));
        switch (input.trim()) {
            case "#help":
                this.showHelp();
                break;
            case "#cls":
                this.items([]);
                break;
            case "#show-log":
                this.showLog();
                break;
            case "#download-log":
                this.client.downloadLog();
                break;
            default:
                this.calculate(input);
                break;
        }
        this.input("");
    }

    showLog() {
        this.items([]);
        this.client.getLog(function (response) {
            for (var i = 0; i < response.length; i++) {
                var log = response[i];
                this.items.push(new Item(">" + log.input));
                var item = new Item(processResponse(log.output));
                if (!log.output.isSuccessful) {
                    item.isError(true);
                }
                this.items.push(item);
            }
        });
    }

    calculate(input) {
        var item = new Item("...");
        this.items.push(item);
        this.client.calculate(input,
            function (response) {
                item.val(processResponse(response));
                if (!response.isSuccessful) {
                    item.isError(true);
                }
            });
    }

    showHelp() {
        this.items.push(new Item("Simple math expressions evaluator."));
        this.items.push(new Item("Input example: 2+3*4"));
        this.items.push(new Item("Supported operations: Binary: +-*/^ Unary: +- Parenthesis: ()"));
        this.items.push(new Item("REPL commands:"));
        this.items.push(new Item("#help - to show help"));
        this.items.push(new Item("#cls - clear screen"));
        this.items.push(new Item("#show-log - show log"));
        this.items.push(new Item("#download-log - to download log of all operations"));
    }
}

export { AppViewModel, Item }