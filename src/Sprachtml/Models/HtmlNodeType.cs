using Sprachtml.Meta;

namespace Sprachtml.Models
{
    public enum HtmlNodeType
    {
        Custom,
        Text,
        Comment,
        DocType,

        A,
        Abbr,
        [ObsoleteDeprecatedOrNonStandard]
        Acronym,
        Address,
        [ObsoleteDeprecatedOrNonStandard]
        Applet,
        [VoidElement]
        Area,
        Article,
        Aside,
        Audio,

        B,
        [VoidElement]
        Base,
        [ObsoleteDeprecatedOrNonStandard]
        Basefont,
        Bdi,
        Bdo,
        [ObsoleteDeprecatedOrNonStandard]
        Bgsound,
        [ObsoleteDeprecatedOrNonStandard]
        Big,
        [ObsoleteDeprecatedOrNonStandard]
        Blink,
        Blockquote,
        Body,
        [VoidElement]
        Br,
        Button,

        Canvas,
        Caption,
        [ObsoleteDeprecatedOrNonStandard]
        Center,
        Cite,
        Code,
        [VoidElement]
        Col,
        Colgroup,
        Content,
        [VoidElement]
        [ObsoleteDeprecatedOrNonStandard]
        Command,

        Data,
        Datalist,
        Dd,
        Del,
        Details,
        Dfn,
        [ObsoleteDeprecatedOrNonStandard]
        Dir,
        Div,
        Dl,
        Dt,

        Em,
        [VoidElement]
        Embed,

        Fieldset,
        Figcaption,
        Figure,
        [ObsoleteDeprecatedOrNonStandard]
        Font,
        Footer,
        Form,
        [ObsoleteDeprecatedOrNonStandard]
        Frame,
        [ObsoleteDeprecatedOrNonStandard]
        Frameset,

        H1,
        H2,
        H3,
        H4,
        H5,
        H6,
        Head,
        Header,
        [ObsoleteDeprecatedOrNonStandard]
        Hgroup,
        [VoidElement]
        Hr,
        Html,

        I,
        Iframe,
        [VoidElement]
        Img,
        [VoidElement]
        Input,
        Ins,
        [ObsoleteDeprecatedOrNonStandard]
        Isindex,

        Kbd,
        [VoidElement]
        Keygen,

        Label,
        Legend,
        Li,
        [VoidElement]
        Link,
        [ObsoleteDeprecatedOrNonStandard]
        Listing,

        Main,
        Map,
        Mark,
        [ObsoleteDeprecatedOrNonStandard]
        Marquee,
        Menu,
        Menuitem,
        [VoidElement]
        Meta,
        Meter,

        Nav,
        [ObsoleteDeprecatedOrNonStandard]
        Nobr,
        [ObsoleteDeprecatedOrNonStandard]
        Noframes,
        Noscript,

        Object,
        Ol,
        Optgroup,
        Option,
        Output,

        P,
        [VoidElement]
        Param,
        [ObsoleteDeprecatedOrNonStandard]
        Plaintext,
        Pre,
        Progress,

        Q,

        Rp,
        Rt,
        Ruby,

        S,
        Samp,
        Script,
        Section,
        Select,
        Shadow,
        Small,
        [VoidElement]
        Source,
        [ObsoleteDeprecatedOrNonStandard]
        Spacer,
        Span,
        [ObsoleteDeprecatedOrNonStandard]
        Strike,
        Strong,
        Style,
        Sub,
        Summary,
        Sup,

        Table,
        Tbody,
        Td,
        Template,
        Textarea,
        Tfoot,
        Th,
        Thead,
        Time,
        Title,
        Tr,
        [VoidElement]
        Track,
        [ObsoleteDeprecatedOrNonStandard]
        Tt,

        U,
        Ul,

        Var,
        Video,

        [VoidElement]
        Wbr,

        [ObsoleteDeprecatedOrNonStandard]
        Xmp,
    }
}