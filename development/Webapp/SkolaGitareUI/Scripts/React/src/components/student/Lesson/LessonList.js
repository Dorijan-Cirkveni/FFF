import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import pink from '@material-ui/core/colors/pink';
import Link from '@material-ui/core/Link';
import GetAppIcon from '@material-ui/icons/GetApp';
import Button from '@material-ui/core/Button';
import TabPanel from "../../shared/TabPanel";
import request from "../../../request.js";


function a11yProps(index) {
    return {
        id: `lesson-tab-${index}`,
        'aria-controls': `lesson-tabpanel-${index}`,
    };
}

const useStyles = makeStyles((theme) => ({
    gridRoot: {
        flexGrow: 4,
    },
    root: {
        flexGrow: 1,
        backgroundColor: theme.palette.background.paper,
        display: 'flex',
        height: '500px',

    },
    tabs: {
        borderRight: `1px solid ${theme.palette.divider}`,

    },
    paper: {
        padding: theme.spacing(2),
        textAlign: 'center',
        color: pink[600],
    },
    button: {
        backgroundColor: pink[500],
        marginTop: '5px',
        color: 'white'
    }
}));

export default function LessonList(props) {
    const classes = useStyles();
    const [value, setValue] = React.useState(0);
    const [lessons, setLessons] = React.useState(null);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };
    console.log(props);

    React.useEffect(() => {
        let mounted = true;
        console.log(props.accessToken);
        request(props.accessToken, {
            method: "get",
            url: `Lessons/Student/${props.user.Id}`,
        }).then((data) => {
            console.log(data);
            if (mounted) {
                setLessons(data);
            }
        });
        return () => (mounted = false);
    }, []);
    return (
        lessons ? 
        <div className={classes.root}>
            <Tabs
                orientation="vertical"
                variant="scrollable"
                value={value}
                onChange={handleChange}
                aria-label="Lista lekcija"
                className={classes.tabs}
            >
                {lessons.map((lesson, i) => {
                    console.log(i);
                    return (
                        <Tab label={lesson.name} {...a11yProps(i)} />
                    )
                })}
            </Tabs>
            {lessons.map((lesson, i) => (
                <TabPanel value={value} index={i}>

                    <Box>
                        <h4 style={{ textTransform: 'uppercase', color: pink[600] }}>{lesson.name}</h4>
                        <Box p={3} m={2}>
                            <h5> Bilješke:</h5>
                            <Box m={1} p={1} style={{ width: '1000px' }}>
                                <Typography> {lesson.notes}</Typography>
                            </Box>
                            <h5>Nastavnik:</h5>
                            <Box m={1} p={1}>
                                <Typography> Teacher Teacher</Typography>
                            </Box>
                            <h5>Datum: </h5>
                            <Box m={1} p={1}>
                                <Typography>{lesson.LessonDate}</Typography>
                            </Box>
                            <Link href={lesson.downloadLink} download style={{ textDecoration: 'none' }}>

                                <Button
                                    variant="contained"
                                    className={classes.button}
                                    startIcon={<GetAppIcon />}
                                    color='secondary'
                                    disableFocusRipple={true}
                                >
                                    Skini materijale
                          </Button>
                            </Link>


                        </Box>

                    </Box>

                </TabPanel>

            ))}
        </div> : <h3>Nema dostupnih lekcija</h3>)
}