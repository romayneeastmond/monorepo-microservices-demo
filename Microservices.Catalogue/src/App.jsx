import { useState } from 'react'
import AppBar from '@mui/material/AppBar'
import Box from '@mui/material/Box'
import CssBaseline from '@mui/material/CssBaseline'
import Divider from '@mui/material/Divider'
import Drawer from '@mui/material/Drawer'
import IconButton from '@mui/material/IconButton'
import List from '@mui/material/List'
import ListItem from '@mui/material/ListItem'
import ListItemIcon from '@mui/material/ListItemIcon'
import ListItemText from '@mui/material/ListItemText'
import MenuIcon from '@mui/icons-material/Menu'
import Toolbar from '@mui/material/Toolbar'
import Typography from '@mui/material/Typography'

import BubbleChartIcon from '@mui/icons-material/BubbleChart'
import DirectionsBusFilledIcon from '@mui/icons-material/DirectionsBusFilled'
import GroupIcon from '@mui/icons-material/Group'
import HomeIcon from '@mui/icons-material/Home'
import MailOutlineIcon from '@mui/icons-material/MailOutline'
import PersonIcon from '@mui/icons-material/Person'
import SchoolIcon from '@mui/icons-material/School'
import WebAssetIcon from '@mui/icons-material/WebAsset'

import './App.scss'

const App = () => {
    const [mobileOpen, setMobileOpen] = useState(false)
    const [currentMicroservice, setCurrentMicroservice] = useState('')
    const [currentMicroserviceName, setCurrentMicroserviceName] = useState('')

    const drawerWidth = 280

    const microservices = [
        'Company.Course',
        'Company.Department',
        'Company.Employee',
        'Company.Notification'
    ]

    const getMicroserviceIcon = (name) => {
        switch (name) {
            case 'Company.Course':
                return <SchoolIcon />
            case 'Company.Department':
                return <GroupIcon />
            case 'Company.Employee':
                return <PersonIcon />
            case 'Company.Notification':
                return <MailOutlineIcon />
            case 'RabbitMQ':
                return <DirectionsBusFilledIcon />
            case 'GraphQL':
                return <BubbleChartIcon />
            default:
                return <WebAssetIcon />
        }
    }

    const handleDrawerToggle = () => {
        setMobileOpen(!mobileOpen)
    }

    const loadMicroservice = (name) => {
        setCurrentMicroserviceName(<>{getMicroserviceIcon(name)} {name}</>)

        switch (name) {
            case 'Company.Course':
                setCurrentMicroservice(process.env.REACT_APP_COMPANY_COURSE)
                break
            case 'Company.Department':
                setCurrentMicroservice(process.env.REACT_APP_COMPANY_DEPARTMENT)
                break
            case 'Company.Employee':
                setCurrentMicroservice(process.env.REACT_APP_COMPANY_EMPLOYEE)
                break
            case 'Company.Notification':
                setCurrentMicroservice(process.env.REACT_APP_COMPANY_NOTIFICATION)
                break
            case 'RabbitMQ':
                setCurrentMicroservice(process.env.REACT_APP_RABBITMQ)
                break;
            case 'GraphQL':
                setCurrentMicroservice(process.env.REACT_APP_GRAPHQL)
                break;
            default:
                setCurrentMicroservice('')
        }
    }

    const drawer = (
        <>
            <Toolbar />
            <Divider />
            <List>
                <ListItem button onClick={() => loadMicroservice('Home')}>
                    <ListItemIcon>
                        <HomeIcon />
                    </ListItemIcon>
                    <ListItemText primary='Home' />
                </ListItem>
                <Divider />

                {microservices.map((text, index) => (
                    <ListItem button key={text} onClick={() => loadMicroservice(text)}>
                        <ListItemIcon>
                            {getMicroserviceIcon(text)}
                        </ListItemIcon>
                        <ListItemText primary={text} />
                    </ListItem>
                ))}

                {
                    (process.env.REACT_APP_RABBITMQ || process.env.REACT_APP_GRAPHQL) &&
                    <>
                        <Divider />
                    </>
                }

                {
                    process.env.REACT_APP_GRAPHQL &&
                    <>
                        <ListItem button onClick={() => loadMicroservice('GraphQL')}>
                            <ListItemIcon>
                                <BubbleChartIcon />
                            </ListItemIcon>
                            <ListItemText primary='Apollo GraphQL' />
                        </ListItem>
                    </>
                }

                {
                    process.env.REACT_APP_RABBITMQ &&
                    <>
                        <ListItem button onClick={() => loadMicroservice('RabbitMQ')}>
                            <ListItemIcon>
                                <DirectionsBusFilledIcon />
                            </ListItemIcon>
                            <ListItemText primary='RabbitMQ' />
                        </ListItem>
                    </>
                }
            </List>
        </>
    )

    return (
        <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <AppBar position='fixed' sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
                <Toolbar>
                    <IconButton color='inherit' aria-label='Open Drawer' edge='start' onClick={handleDrawerToggle} sx={{ mr: 2, display: { sm: 'none' } }}>
                        <MenuIcon />
                    </IconButton>
                    <Typography variant='h6' noWrap component='div'>
                        Microservices.Catalogue
                    </Typography>
                </Toolbar>
            </AppBar>

            <Box component='nav' sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }} aria-label='SwaggerUI Endpoints'>
                <Drawer
                    variant='temporary'
                    ModalProps={{
                        keepMounted: true,
                    }}
                    sx={{ display: { xs: 'block', sm: 'none' }, '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth } }}
                    open={mobileOpen}
                    onClose={handleDrawerToggle}
                >
                    {drawer}
                </Drawer>
                <Drawer variant='permanent' sx={{ display: { xs: 'none', sm: 'block' }, '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth } }} open>
                    {drawer}
                </Drawer>
            </Box>
            <Box component='main' sx={{ flexGrow: 1, p: 3, width: { sm: `calc(100% - ${drawerWidth}px)` } }}>
                <Toolbar />

                {
                    currentMicroservice.length === 0 &&
                    <Typography component='h1' variant='h6' gutterBottom>
                        Hello World from Microservices.Catalogue!
                    </Typography>
                }

                {
                    currentMicroservice.length > 0 &&
                    <>
                        <Typography component='h1' variant='h6' gutterBottom>
                            {currentMicroserviceName}
                        </Typography>
                        <Typography component='h6' gutterBottom>
                            <a href={currentMicroservice} target='_blank' rel='noreferrer'>{currentMicroservice}</a>
                        </Typography>
                        <iframe className='microservice' title='Microservice Preview' src={currentMicroservice} scrolling='no'>

                        </iframe>
                    </>
                }
            </Box>
        </Box>
    )
}

export default App