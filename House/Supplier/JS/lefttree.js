$(function() { $("#wnav").accordion({ animate: true }); var firstMenuName = "basic"; addNav(_menus[firstMenuName]); InitLeftMenu(); }); function addNav(data) {
    $.each(data, function(i, sm) {
        var menulist = ""; menulist += '<ul>'; $.each(sm.menus, function(j, o) {
            menulist += '<li><div><a ref="' + o.menuid + '" href="#" rel="'
+ o.url + '" ><span class="icon ' + o.icon
+ '" >&nbsp;</span><span class="nav">' + o.menuname
+ '</span></a></div></li> ';
        }); menulist += '</ul>'; $('#wnav').accordion('add', { title: sm.menuname, content: menulist, iconCls: 'icon ' + sm.icon });
    }); var pp = $('#wnav').accordion('panels'); var t = pp[0].panel('options').title; $('#wnav').accordion('select', t);
}
function InitLeftMenu() { $('#wnav li a').click(function() { var tabTitle = $(this).children('.nav').text(); var url = $(this).attr("rel"); var menuid = $(this).attr("ref"); var icon = getIcon(menuid, icon); addTab(tabTitle, url, icon); $('#wnav li div').removeClass("selected"); $(this).parent().addClass("selected"); }); }
function getIcon(menuid) { var icon = 'icon '; $.each(_menus, function(i, n) { $.each(n, function(j, o) { $.each(o.menus, function(k, m) { if (m.menuid == menuid) { icon += m.icon; return false; } }); }); }); return icon; }
function addTab(subtitle, url, icon) { if (!$('#tt').tabs('exists', subtitle)) { $('#tt').tabs('add', { title: subtitle, content: createFrame(url), closable: true, icon: icon }); } else { $('#tt').tabs('select', subtitle); $('#mm-tabupdate').click(); } }
function selectTab(subtitle) { $('#tt').tabs('select', subtitle); $('#mm-tabupdate').click(); }
function createFrame(url) { var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>'; return s; }