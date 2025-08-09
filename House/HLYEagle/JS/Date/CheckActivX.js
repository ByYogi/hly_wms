function CheckLodop() { var a = LODOP.Version; newVerion = "4.0.1.2", null == a ? (document.write("<h3><font color='#FF00FF'>打印控件未安装!点击这里<a href='install_lodop.exe'>执行安装</a>,安装后请刷新页面。</font></h3>"), "Netscape" == navigator.appName && document.write("<h3><font color='#FF00FF'>（Firefox浏览器用户需先点击这里<a href='npActiveX0712SFx31.xpi'>安装运行环境</a>）</font></h3>")) : newVerion > a && document.write("<h3><font color='#FF00FF'>打印控件需要升级!点击这里<a href='install_lodop.exe'>执行升级</a>,升级后请重新进入。</font></h3>") } function getLodop(a, b) { var i, j, c = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='install_lodop32.exe' target='_self'>执行安装</a>,安装后请刷新页面或重新进入。</font>", d = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='install_lodop32.exe' target='_self'>执行升级</a>,升级后请重新进入。</font>", e = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='install_lodop64.exe' target='_self'>执行安装</a>,安装后请刷新页面或重新进入。</font>", f = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='install_lodop64.exe' target='_self'>执行升级</a>,升级后请重新进入。</font>", g = "<br><br><font color='#FF00FF'>注意：<br>1：如曾安装过Lodop旧版附件npActiveXPLugin,请在【工具】->【附加组件】->【扩展】中先卸它。</font>", h = b; try { return i = navigator.userAgent.indexOf("MSIE") >= 0 || navigator.userAgent.indexOf("Trident") >= 0, j = i && navigator.userAgent.indexOf("x64") >= 0, i && (h = a), null == h || "undefined" == typeof h.VERSION ? (navigator.userAgent.indexOf("Firefox") >= 0 && (document.documentElement.innerHTML = g + document.documentElement.innerHTML), j ? document.write(e) : i ? document.write(c) : document.documentElement.innerHTML = c + document.documentElement.innerHTML, h) : h.VERSION < "6.1.5.9" ? (j ? document.write(f) : i ? document.write(d) : document.documentElement.innerHTML = d + document.documentElement.innerHTML, h) : (h.SET_LICENSES("", "780101871115550100112112561289", "688858710010010811411756128900", "947425255495357625563605612890"), h) } catch (k) { return document.documentElement.innerHTML = j ? "Error:" + e + document.documentElement.innerHTML : "Error:" + c + document.documentElement.innerHTML, h } }



function OpenPrinter() {
    PSKPrn.OpenPort("POSTEK");
    PSKPrn.PTKClearBuffer();
    PSKPrn.PTKSetPrintSpeed(6);
    PSKPrn.PTKSetDarkness(14);
    PSKPrn.PTKSetLabelHeight(590, 16, 0, 0);
    PSKPrn.PTKSetLabelWidth(941);
}
function ClosePrinter() {
    PSKPrn.ClosePort();
}