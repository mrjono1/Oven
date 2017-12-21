using MasterBuilder.Helpers;

namespace MasterBuilder.Templates.ClientApp.App
{
    /// <summary>
    /// app.component.scss used for application styling
    /// </summary>
    public class AppComponentScssTemplate : ITemplate
    {
        public string GetFileName()
        {
            return "app.component.scss";
        }

        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "app" };
        }
        public string GetFileContent()
        {
            return @"$navbar-default-bg: #312312;
$light-orange: #ff8c00;
$navbar-default-color: $light-orange;

/* Import Bootstrap & Fonts */
$icon-font-path: '~bootstrap-sass/assets/fonts/bootstrap/';
@import ""~bootstrap-sass/assets/stylesheets/bootstrap"";

/* *** Overall APP Styling can go here ***
   --------------------------------------------
   Note: This Component has ViewEncapsulation.None so the styles will bleed out
*/
@media (max-width: 767px) {
  body {
    background: #f1f1f1;
    line-height: 18px;
    padding-top: 30px;
  }

  h1 {
    border-bottom: 3px #4189C7 solid;
    font-size: 24px;
  }

  h2 {
    font-size: 20px;
  }

  h1, h2, h3 {
    padding: 3px 0;
  }
}

@media (min-width: 768px) {
  body {
    background: #f1f1f1;
    line-height: 18px;
    padding-top: 0px;
  }

  h1 {
    border-bottom: 5px #4189C7 solid;
    font-size: 36px;
  }

  h2 {
    font-size: 30px;
  }

  h1, h2, h3 {
    padding: 10px 0;
  }
}

ul { padding: 10px 25px; }
ul li { padding: 5px 0; }

blockquote { margin: 25px 10px; padding: 10px 35px 10px 10px; border-left: 10px #158a15 solid; background: #edffed; }
blockquote a, blockquote a:hover { color: #068006; }



/* TO remove*/
li .glyphicon {
    margin-right: 10px;
}

/* Highlighting rules for nav menu items */
li.link-active a,
li.link-active a:hover,
li.link-active a:focus {
    background-color: #4189C7;
    color: white;
}

/* Keep the nav menu independent of scrolling and on top of other items */
.main-nav {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    z-index: 1;
}

@media (min-width: 768px) {
    /* On small screens, convert the nav menu to a vertical sidebar */
    .main-nav {
        height: 100%;
        width: calc(25% - 20px);
    }
    .navbar {
        border-radius: 0;
        border-width: 0;
        height: 100%;
    }
    .navbar-header {
        float: none;
    }
    .navbar-collapse {
        border-top: 1px solid #444;
        padding: 0;
    }
    .navbar ul {
        float: none;
    }
    .navbar li {
        float: none;
        font-size: 15px;
        margin: 6px;
    }
    .navbar li a {
        padding: 10px 16px;
        border-radius: 4px;
    }
    .navbar a {
        /* If a menu item's text is too long, truncate it */
        width: 100%;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }
}
";
        }
    }
}
