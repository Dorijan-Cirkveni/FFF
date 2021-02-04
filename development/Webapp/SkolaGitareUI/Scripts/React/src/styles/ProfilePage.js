import React from 'react';
import { makeStyles } from '@material-ui/core/styles';

import guitar from '../pictures/Guitars-PixTeller.png';



const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexWrap: 'wrap',
        '& > *': {
            margin: theme.spacing(1),
            width: theme.spacing(16),
            height: theme.spacing(16),
        },
    },
    paper: {
        width: "100%",
        marginTop: "-200px",
        height: '500px',
        marginLeft: "50px",
        marginRight: "50px",
    },
    header: {
        height: "350px",
        margin: 0,
        padding: 0,
        border: 0,
        display: 'flex',
        alignItems: 'center',
        backgroundImage: `url(${guitar})`,
        //backgroundImage: "linear-gradient(to bottom, #343638, #626365, #939495, #c8c8c9, #ffffff)",
        width: '100%',
        backgroundSize: "40%"
    },
    avatar: {
        width: theme.spacing(22),
        height: theme.spacing(22),
        display: 'block',
        marginLeft: 'auto',
        marginRight: 'auto',
    },
    avatarHolder: {
        margin: '-60px',
        alignContent: 'center'
    },
    content: {
        margin: '50px',
        marginTop: '100px',
    },
    info: {
        textAlign: 'center',
    },

    card: {
        maxWidth: 345,
    },
    nameText: {
        letterSpacing: '1.5px',

    },
    roleText: {
        textTransform: 'uppercase',
        opacity: '60%',
        letterSpacing: '1.5px',
        size: '70%',
        margin: '20px',
        lineHeight: '1.3'
    },
    iconText: {

    },
    phoneEmail: {
        textTransform: 'uppercase',
        opacity: '60%',
        letterSpacing: '1.5px',
        size: '10%',
        margin: '5px',
        lineHeight: '1.3',
        fontSize: 'small'
    },
    buttonGroup: {
        '& > *': {
            margin: theme.spacing(1),
        },
    },
    papperTabs: {
        flexGrow: 4,
        margin: '30px'
    },
    tabs: {
        transform: 'scale(0.2)'
    },
    tabRoot: {
        minHeight: '15px'
    }

}));

export default useStyles;