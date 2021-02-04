
import { makeStyles } from '@material-ui/core/styles';
import guitar from '../pictures/Guitars-PixTeller.png';

const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexWrap: 'wrap',
        '& > *': {
            margin: theme.spacing(1),
            width: theme.spacing(16),
        },
    },
    paper: {
        width: "100%",
        marginTop: "-320px",
        height: '600px',
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
        width: '100%',
        backgroundSize: "40%",
        zIndex: '900px',

    },

}));

export default useStyles;