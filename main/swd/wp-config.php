<?php
/**
 * WordPress基础配置文件。
 *
 * 本文件包含以下配置选项：MySQL设置、数据库表名前缀、密钥、
 * WordPress语言设定以及ABSPATH。如需更多信息，请访问
 * {@link http://codex.wordpress.org/zh-cn:%E7%BC%96%E8%BE%91_wp-config.php
 * 编辑wp-config.php}Codex页面。MySQL设置具体信息请咨询您的空间提供商。
 *
 * 这个文件被安装程序用于自动生成wp-config.php配置文件，
 * 您可以手动复制这个文件，并重命名为“wp-config.php”，然后填入相关信息。
 *
 * @package WordPress
 */

// ** MySQL 设置 - 具体信息来自您正在使用的主机 ** //
/** WordPress数据库的名称 */
define('DB_NAME', 'swd');

/** MySQL数据库用户名 */
define('DB_USER', 'root');

/** MySQL数据库密码 */
define('DB_PASSWORD', '123456');

/** MySQL主机 */
define('DB_HOST', 'localhost');

/** 创建数据表时默认的文字编码 */
define('DB_CHARSET', 'utf8');

/** 数据库整理类型。如不确定请勿更改 */
define('DB_COLLATE', '');

/**#@+
 * 身份认证密钥与盐。
 *
 * 修改为任意独一无二的字串！
 * 或者直接访问{@link https://api.wordpress.org/secret-key/1.1/salt/
 * WordPress.org密钥生成服务}
 * 任何修改都会导致所有cookies失效，所有用户将必须重新登录。
 *
 * @since 2.6.0
 */
define('AUTH_KEY',         'F6NID?LlNBDzay>98$(mE8A2i.8%n$b}rl::Q>Nc5gp7hBI-WR7cFuuoWDeapHpk');
define('SECURE_AUTH_KEY',  'X(f=4l&4b0ByP] 0322-qt~?}wv*zh|0fG(eb*I{YeMZ!~*chnq}(mw-ykK.Z#B-');
define('LOGGED_IN_KEY',    'UjwEO.p$^#=ENWtsltNd$o6CVHvk+uRl)b:IjxF+:Q|}|G~~.+!X^:NLm[n0{eP`');
define('NONCE_KEY',        'Q%j#Ad?+bXMyJRT;|Z,QQq:@%RHvh-)^7>7[2G!W@+D.e9EbDlvp7;^$o 5{76!o');
define('AUTH_SALT',        'x!t9rAc55A{@|9 f:iX;5z%IAbyN@<4FRD+9GAq48kJ|DhB4NrF(Atrl!+xb`~M<');
define('SECURE_AUTH_SALT', 'F</pnDv8&(D*)|R4zC=-/yH+<3j`9Zb:!BCp8|sD+2l8s/pcoGVOu!RLC#R;daR$');
define('LOGGED_IN_SALT',   'Y-]8|e/c~@h!|dE-tm[MsczI/]LPj|U11K*]rrx |+WfCVj*kYaT~P$cuG+c/&3o');
define('NONCE_SALT',       'Cga/1^N[*W{dyaSW|WN+]+N.LpI_=ht:SJq{hGo$vp?X(P1t)EyQX-DZfC5iW.[E');

/**#@-*/

/**
 * WordPress数据表前缀。
 *
 * 如果您有在同一数据库内安装多个WordPress的需求，请为每个WordPress设置
 * 不同的数据表前缀。前缀名只能为数字、字母加下划线。
 */
$table_prefix  = 'swd_';

/**
 * 开发者专用：WordPress调试模式。
 *
 * 将这个值改为true，WordPress将显示所有用于开发的提示。
 * 强烈建议插件开发者在开发环境中启用WP_DEBUG。
 */
define('WP_DEBUG', false);

/**
 * zh_CN本地化设置：启用ICP备案号显示
 *
 * 可在设置→常规中修改。
 * 如需禁用，请移除或注释掉本行。
 */
define('WP_ZH_CN_ICP_NUM', true);

/* 好了！请不要再继续编辑。请保存本文件。使用愉快！ */

/** WordPress目录的绝对路径。 */
if ( !defined('ABSPATH') )
	define('ABSPATH', dirname(__FILE__) . '/');

/** 设置WordPress变量和包含文件。 */
require_once(ABSPATH . 'wp-settings.php');
