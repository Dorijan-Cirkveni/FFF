import React from 'react';
import Paper from '@material-ui/core/Paper';
import Avatar from '@material-ui/core/Avatar';
import PhoneIcon from '@material-ui/icons/Phone';
import MailOutlineIcon from '@material-ui/icons/MailOutline';

import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/DeleteForever';

import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Button from '@material-ui/core/Button';

import EditProfile from "./EditProfile";
import TabPanel from './TabPanel';
import useStyles from '../../styles/ProfilePage';
import request from "../../request.js";



function a11yProps(index) {
    return {
        id: `profile-tab-${index}`,
        'aria-controls': `profile-tabpanel-${index}`,
    };
}
export default function ProfilePage(props) {
    const classes = useStyles();
    const [value, setValue] = React.useState(null);
    const [data, setData] = React.useState(null);

    const handleChange = (event, newValue) => {
        (value === newValue) ? setValue(null) : setValue(newValue);

    };
    React.useEffect(() => {
        let mounted = true;
        request(props.accessToken, {
            method: 'post',
            url: 'User/DownloadPersonalData',

            
        }).then(data => {
                if (mounted) {
                    setData(data);
                }
            })
        return () => mounted = false;
    }, [])
    return (
        data ? 
        <div className={classes.root}>
            <div className={classes.header}></div>

            <Paper elevation={3} className={classes.paper}>
                <div className={classes.avatarHolder}>

                    <Avatar alt="Remy Sharp" src="../pictures/Guitars-PixTeller.png" className={classes.avatar} />

                </div>
                <div className={classes.content}>
                    <div className={classes.info}>

                            <h3 className={classes.nameText}>{data.UserName}</h3>
                            <h6 className={classes.roleText}>{props.user.role}</h6>
                        <div style={{ display: 'inline-flex' }}>
                            <div className={classes.iconText}>
                                    <MailOutlineIcon style={{ opacity: '60%' }} />
                                    <h7 className={classes.phoneEmail} >{data.Email}</h7>
                            </div>
                            <div className={classes.iconText}>
                                <PhoneIcon style={{ opacity: '60%' }} />
                                    <h7 className={classes.phoneEmail}>{data.PhoneNumber ? data.PhoneNumber : 'Nema broja'}</h7>
                            </div>
                        </div>
                        <div>
                            <Paper square className={classes.papperTabs}>
                                <Tabs
                                    value={value}
                                    onChange={handleChange}
                                    indicatorColor="secondary"
                                    textColor="secondary  "
                                    aria-label="icon tabs example"
                                    centered
                                    color="secondary"
                                >
                                    <Tab icon={<EditIcon style={{ fontSize: 20 }} />} aria-label="phone" {...a11yProps(0)} />
                                    <Tab icon={<DeleteIcon style={{ fontSize: 20 }} />} aria-label="person" {...a11yProps(1)} />
                                </Tabs>
                            </Paper>
                            <TabPanel value={value} index={0}>
                                <EditProfile />
                            </TabPanel>
                            <TabPanel value={value} index={1}>
                                Želite li izbrisati račun?
                                <Button>DA</Button>
                            </TabPanel>

                        </div>


                    </div>

                </div>

            </Paper>
            </div> :
            <div>Došlo je do greške</div>
    );
}
