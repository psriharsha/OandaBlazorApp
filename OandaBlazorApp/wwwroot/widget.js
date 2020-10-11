var AppWindows = new Array();
const blazorWidget = {
    open: (url, title, prop, obj) => {
        AppWindows["__main"] = window;
        blazorWidget.setupKill(window);
        if (!blazorWidget.winExists(title)) {
            var w = window.open(url, title, prop);
            if (w != undefined) {
                AppWindows[title] = w;
                w.onclose = () => {
                    obj.invokeMethodAsync('NotifyWindowClosed', title).then(() => console.log(title + " window closed"));
                }
            }
        }
    },
    setupKill: (win) => {
        win.onbeforeunload = () => {
            AppWindows.forEach(i => i.close());
        }
    },
    winExists: (win) => {
        return AppWindows[win] != undefined;
    }
}

window['blazorWidget'] = blazorWidget;